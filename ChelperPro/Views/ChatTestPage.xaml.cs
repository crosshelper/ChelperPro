using ChelperPro.Helpers;
using ChelperPro.Models;
using ChelperPro.ViewModels;
using SendBird;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChelperPro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatTestPage : ContentPage
	{
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        public ChatTestPage (UserInfo user, GroupChannel channel)
		{
			InitializeComponent ();
            ChatTestViewModel vvm;
            this.BindingContext = vvm = new ChatTestViewModel();
            vvm.Channel = channel;
            vvm.Title = user.FirstName + " " + user.LastName;
            vvm.Load();
        }

        public void ScrollTap(object sender, System.EventArgs e)
        {
            lock (new object())
            {
                if (BindingContext != null)
                {
                    var vm = BindingContext as ChatTestViewModel;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        while (vm.DelayedMessages.Count > 0)
                        {
                            vm.Messages.Insert(0, vm.DelayedMessages.Dequeue());
                        }
                        vm.ShowScrollTap = false;
                        vm.LastMessageVisible = true;
                        vm.PendingMessageCount = 0;
                        ChatList?.ScrollToFirst();
                    });
                }
            }
        }

        public void OnListTapped(object sender, ItemTappedEventArgs e)
        {
            chatInput.UnFocusEntry();
        }
    }
}