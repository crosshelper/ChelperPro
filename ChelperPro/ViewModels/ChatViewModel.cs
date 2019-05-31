using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ChelperPro.Models;
using ChelperPro.Views;
using SendBird;
using Xamarin.Forms;
using static SendBird.SendBirdClient;

namespace ChelperPro.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        #region Properties
        private UserInfo userTo;

        public UserInfo UserTo
        {
            get { return userTo; }
            set { SetProperty(ref userTo, value); }
        }

        private ObservableCollection<UserMessage> _messages;

        public ObservableCollection<UserMessage> Messages
        {
            get { return _messages; }
            set { SetProperty(ref _messages, value); }
        }

        private string txtmessage;
        public string TxtMessage
        {
            get { return txtmessage; }
            set { SetProperty(ref txtmessage, value); }
        }

        private GroupChannel channel;

        public GroupChannel Channel
        {
            get { return channel; }
            set { SetProperty(ref channel, value); }
        }

        private string username;

        public string UserName
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }


        public ICommand SendCommand { get; private set; }

        #endregion
        private ChannelHandler ch = new ChannelHandler();
        public ChatViewModel()
        {
            SendCommand = new Command(Send);
            Messages = new ObservableCollection<UserMessage>();
            //ChatPage.CurrentActivity.ScrollDown(Messages.Last());
            //RemoveChannelHandler("MyKey");
        }

        public void Init()
        {
            var seconds = TimeSpan.FromSeconds(1);
            Device.StartTimer(seconds, () => {
                ch.OnMessageReceived = (BaseChannel baseChannel, BaseMessage baseMessage) => {
                    Messages.Add((UserMessage)baseMessage);
                    //ChatPage.CurrentActivity.ScrollDown(Messages.Last());1
                };
                return true;
            });
            AddChannelHandler("MyKey", ch);
        }

        public async void Load()
        {
            IsBusy = true;
            await Task.Delay(1000);
            PreviousMessageListQuery mPrevMessageListQuery = Channel.CreatePreviousMessageListQuery();
            mPrevMessageListQuery.Load(30, true, (List<BaseMessage> messages, SendBirdException e) => {
                if (e != null)
                {
                    // Error.
                    return;
                }
                messages = messages.OrderByDescending(x => x.CreatedAt).ToList();
                foreach (var item in messages)
                {
                    Messages.Insert(0, (UserMessage)item);
                }
                //ChatPage.CurrentActivity.ScrollDown(Messages.Last());
            });
            IsBusy = false;
            Init();
        }

        public void Send()
        {
            IsBusy = true;
            try
            {
                if (string.IsNullOrEmpty(TxtMessage))
                {
                    IsBusy = false; return;
                }
                Channel.SendUserMessage(TxtMessage, "", (UserMessage userMessage, SendBirdException e) =>
                {
                    if (e != null)
                    {
                        // Error.
                        return;
                    }
                    //userMessage.Sender.Nickname;
                    //userMessage.Sender.ProfileUrl;
                    Messages.Add(userMessage);
                    //ChatPage.CurrentActivity.ScrollDown(Messages.Last());
                });
                TxtMessage = string.Empty;
                //ChatPage.CurrentActivity.ScrollDown(Messages.Last());
                IsBusy = false;
            }
            catch (Exception ex)
            {
                ex.ToString();
                IsBusy = false;
            }
        }
    }
}
