using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChelperPro.Helpers;
using ChelperPro.Models;
using SendBird;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class SignInPasswordPage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await System.Threading.Tasks.Task.Delay(150);
                pwdEntry.Focus();
            });
        }
        private string Name = "";
        private string ProfileIcon = "";
        UserInfo usr = new UserInfo();
        UserAccess userAccess = new UserAccess();
        KeyChainHelper kch = new KeyChainHelper();
        private string _currentNumber = "";

        public SignInPasswordPage(string currentNumber)
        {
            InitializeComponent();
            _currentNumber = currentNumber;
        }
        async void Handle_Next(object sender, EventArgs e)
        {
            if (pwdEntry.Text == null)
            {
                DisplayAlert("Notice", "Please enter your password and make sure it's least 8 characters. ", "OK");
                return;
            }
            await Task.Delay(2000);

            if (userAccess.VerifyUser(_currentNumber, pwdEntry.Text))
            {
                kch.SavetoSecureStorage("token_of_" + _currentNumber, pwdEntry.Text);
                //signInloading.Text = "Sign In Succeeded, Data Loading...";
                //signInloading.TextColor = Color.FromHex("#555555");
                Settings.UserId = userAccess.CurrentUid.ToString();
                usr = userAccess.GetUserInfo(userAccess.CurrentUid);
                Settings.ChatID = usr.ChatID;
                Name = usr.FirstName + " " + usr.LastName;
                ProfileIcon = usr.Icon;
                ChatServerConnect();
                await Task.Delay(3000);
                Settings.IsLogin = true;
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                Settings.IsLogin = false;
            }
        }

        private void ChatServerConnect()
        {
            SendBirdClient.Connect(Settings.ChatID, (User user, SendBirdException e) =>
            {
                if (e != null)
                {
                    return;
                }

                SendBirdClient.UpdateCurrentUserInfo(Name, ProfileIcon, (SendBirdException e1) =>
                {
                    if (e1 != null)
                    {
                        return;
                    }
                });

                SendBirdClient.RegisterAPNSPushTokenForCurrentUser(SendBirdClient.GetPendingPushToken(), (SendBirdClient.PushTokenRegistrationStatus status, SendBirdException e1) =>
                {
                    if (e1 != null)
                    {
                        // Error.
                        return;
                    }

                    if (status == SendBirdClient.PushTokenRegistrationStatus.PENDING)
                    {
                        // Try registration after connection is established.
                    }
                });
            });
            Settings.IsLogin = true;
        }


        void Handle_forgotPwd(object sender, System.EventArgs e)
        {
            userAccess.TwilioVerifyService(_currentNumber);
            Navigation.PushAsync(new ResetPWDVerifyPage(_currentNumber));
        }


        void Handle_Privacy(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.cycbis.com/#/privacy"));
            //Navigation.PushModalAsync(new PrivacyPage());
        }
        void Handle_Terms(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.cycbis.com/#/terms"));
            //Navigation.PushModalAsync(new PrivacyPage());
        }
    }
}
