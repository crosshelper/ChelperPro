using ChelperPro.Views;
using SendBird;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static SendBird.SendBirdClient;

namespace ChelperPro.ViewModels
{
    public class ChatTestViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public bool ShowScrollTap { get; set; } = false;
        public bool LastMessageVisible { get; set; } = true;
        public int PendingMessageCount { get; set; } = 0;
        public bool PendingMessageCountVisible { get { return PendingMessageCount > 0; } }

        public Queue<UserMessage> DelayedMessages { get; set; } = new Queue<UserMessage>();
        public ObservableCollection<UserMessage> Messages { get; set; } = new ObservableCollection<UserMessage>();
        public string TextToSend { get; set; }
        public ICommand OnSendCommand { get; set; }
        public ICommand MessageAppearingCommand { get; set; }
        public ICommand MessageDisappearingCommand { get; set; }

        //private GroupChannel channel;
        public GroupChannel Channel { get; set; }

        public ChatTestViewModel()
        {
            MessageAppearingCommand = new Command<UserMessage>(OnMessageAppearing);
            MessageDisappearingCommand = new Command<UserMessage>(OnMessageDisappearing);
            OnSendCommand = new Command(Send);
        }

        private readonly ChannelHandler ch = new ChannelHandler();

        public void Init()
        {
            var seconds = TimeSpan.FromSeconds(1);
            Device.StartTimer(seconds, () => {
                //if (LastMessageVisible)
                //{
                    ch.OnMessageReceived = (BaseChannel baseChannel, BaseMessage baseMessage) => {
                        Messages.Insert(0, (UserMessage)baseMessage);
                        //Messages.Add((UserMessage)baseMessage);
                    };
                //}
                //else
                //{
                    //ch.OnMessageReceived = (BaseChannel baseChannel, BaseMessage baseMessage) => {
                        //DelayedMessages.Enqueue((UserMessage)baseMessage);
                        //PendingMessageCount++;
                    //};
                //}
                return true;
            });
            AddChannelHandler("MyKey", ch);
        }

        public async void Load()
        {
            IsBusy = true;
            await Task.Delay(3000);
            PreviousMessageListQuery mPrevMessageListQuery = Channel.CreatePreviousMessageListQuery();
            mPrevMessageListQuery.Load(30, true, (List<BaseMessage> messages, SendBirdException e) => {
                if (e != null)
                {
                    // Error.
                    return;
                }
                messages = messages.OrderBy(x => x.CreatedAt).ToList();
                foreach (var item in messages)
                {
                    Messages.Insert(0, (UserMessage)item);
                }
            });
            IsBusy = false;
            Init();
        }

        public void Send()
        {
            IsBusy = true;
            try
            {
                if (string.IsNullOrEmpty(TextToSend))
                {
                    IsBusy = false; return;
                }
                Channel.SendUserMessage(TextToSend, "", (UserMessage userMessage, SendBirdException e) =>
                {
                    if (e != null)
                    {
                        // Error.
                        return;
                    }
                    Messages.Insert(0, userMessage);
                });
                TextToSend = string.Empty;
                IsBusy = false;
            }
            catch (NullReferenceException ex)
            {
                ex.ToString();
                IsBusy = false;
            }
        }

        void OnMessageAppearing(UserMessage message)
        {
            if(LastMessageVisible)
            {
                var idx = Messages.IndexOf(message);
                if (idx <= 2)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        while (DelayedMessages.Count > 0)
                        {
                            Messages.Insert(0, DelayedMessages.Dequeue());
                        }
                        ShowScrollTap = false;
                        LastMessageVisible = true;
                        PendingMessageCount = 0;
                    });
                }
            }
        }

        void OnMessageDisappearing(UserMessage message)
        {
            var idx = Messages.IndexOf(message);
            if (idx >= 2)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ShowScrollTap = true;
                    LastMessageVisible = false;
                });

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}