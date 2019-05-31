using System;
using System.Threading.Tasks;
using ChelperPro.Helpers;
using ChelperPro.Models;
using SendBird;
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
            UserAccess userAccess = new UserAccess();
            UserInfo usr = new UserInfo();

            if (userAccess.VerifyUser(uNameEntry.Text, pwdEntry.Text))
            {
                //activity.IsEnabled = true;
                //activity.IsRunning = true;
                //activity.IsVisible = true;
                signInTest.Text = "Sign In Succeeded, Data Loading...";
                signInTest.TextColor = Color.FromHex("#FF4E18");
                Settings.UserId = userAccess.CurrentUid.ToString();
                usr = userAccess.GetUserInfo(userAccess.CurrentUid);
                Settings.ChatID = usr.ChatID;
                Name = usr.FirstName + " " + usr.LastName;
                ChatServerConnect();
                await Task.Delay(3000);
                Application.Current.MainPage = new MainPage();

            }
            else
            {
                signInTest.Text = "Sign in Faild";
                Settings.IsLogin = false;
            }
        }

        private string Name = "";

        private void ChatServerConnect()
        {
            SendBirdClient.Connect(Settings.ChatID, (SendBird.User user, SendBirdException e) =>
            {
                if (e != null)
                {
                    return;
                }

                SendBirdClient.UpdateCurrentUserInfo(Name, "", (SendBirdException e1) =>
                {
                    if (e1 != null)
                    {
                        return;
                    }
                });
            });
            Settings.IsLogin = true;
        }

        //注册按钮 Sign Up
        void Handle_CreateAccount(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SignUpPage();
        }
        //第三次登入 Third party sign in
        void SIPGoogleSignInIcon(object sender, EventArgs e)
        {
            (sender as Button).Text = "Click me again!";
        }
        void SIPWechatSignInIcon(object sender, EventArgs e)
        {
            (sender as Button).Text = "Click me again!";
        }
        void SIPFaceBookSignInIcon(object sender, EventArgs e)
        {
            (sender as Button).Text = "Click me again!";
        }
        //忘记密码 forgot password
        void Handle_ForgotPassword(object sender, EventArgs e)
        {
            Application.Current.MainPage = new ForgotPasswordPage();
        }
        //登入邮箱&密码输入框 Sign in email&password input box
        void SIPSignInEmailCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
        }
        void SIPSignInPasswordCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
        }
    }
}
