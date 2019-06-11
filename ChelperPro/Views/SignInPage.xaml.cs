using System;
using System.Threading.Tasks;
using ChelperPro.Helpers;
using ChelperPro.Models;
using SendBird;
using WebSocketSharp;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class SignInPage : ContentPage
    {

        public SignInPage()
        {
            InitializeComponent();
        }
        //登入
        async void Handle_SignIn(object sender, EventArgs e)
        {
            activity.IsEnabled = true;
            activity.IsRunning = true;
            activity.IsVisible = true;
            signInloading.Text = "Connecting...";
            signInloading.TextColor = Color.FromHex("#FF4E18");
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("No Internet", "Try again later!", "OK");
                return;
            }

            if (uNameEntry.Text.IsNullOrEmpty() || pwdEntry.Text.IsNullOrEmpty())
            {
                await DisplayAlert("No user found", "Try again later!", "OK");
                return;
            }

            UserAccess userAccess = new UserAccess();
            UserInfo usr = new UserInfo();

            if (userAccess.VerifyUser(uNameEntry.Text, pwdEntry.Text))
            {
                if(userAccess.CheckPermission())
                {
                    activity.IsEnabled = true;
                    activity.IsRunning = true;
                    activity.IsVisible = true;
                    signInloading.Text = "Sign In Succeeded, Data Loading...";
                    signInloading.TextColor = Color.FromHex("#FF4E18");
                    Settings.UserId = userAccess.CurrentUid.ToString();
                    usr = userAccess.GetUserInfo(userAccess.CurrentUid);
                    Settings.ChatID = usr.ChatID;
                    Name = usr.FirstName + " " + usr.LastName;
                    ProfileIcon = usr.Icon;
                    ChatServerConnect();
                    await Task.Delay(3000);
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    signInloading.Text = "Permission Denied, Please Signup as a helper.";
                    activity.IsEnabled = false;
                    activity.IsRunning = false;
                    activity.IsVisible = false;
                    Settings.IsLogin = false;
                    await Navigation.PushModalAsync(new SignUpTwoPage());
                }
            }
            else
            {
                signInloading.Text = "Sign in Faild";
                signInloading.TextColor = Color.FromHex("#FF0000");
                activity.IsEnabled = false;
                activity.IsRunning = false;
                activity.IsVisible = false;
                Settings.IsLogin = false;
            }
        }

        private string Name = "";
        private string ProfileIcon = "";

        private void ChatServerConnect()
        {
            SendBirdClient.Connect(Settings.ChatID, (SendBird.User user, SendBirdException e) =>
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

        //第三次登入 Third party sign in
        void Handle_GoogleSignIn(object sender, EventArgs e)
        {
            DisplayAlert("Sorry", "It will be suppported later.", "OK");
        }
        void Handle_FaceBookSignIn(object sender, EventArgs e)
        {
            DisplayAlert("Sorry", "It will be suppported later.", "OK");
        }
        //创建和忘记 Create&Forgot
        void Handle_ForgotPassword(object sender, EventArgs e)
        {
            DisplayAlert("Email Us", "Please send your email address or your phone number to bo.chen@cycbis.com", "OK");
            //Navigation.PushAsync(new ForgotPasswordPage());
        }
        void Handle_CreateAccount(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }

        //取消按钮 Canceled
        void Handle_Canceled(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        //登入邮箱&密码输入框 Sign in email&password input box
        void UsernameSignInCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
        }
        void PasswordSignInCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
        }
        void Handle_Privacy(object sender, System.EventArgs e)
        {
                Device.OpenUri(new Uri("https://cycbis.flycricket.io/privacy.html"));
            //Navigation.PushModalAsync(new PrivacyPage());
        }
    }
}
