using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
        }
        //返回按钮 Go Back
        void SIPSignInGoBack(object sender, EventArgs e)
        {
            (sender as Button).Text = "Click me again!";
        }
        //登入按钮 Sign In
        void Handle_CreateAccount(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
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
        void SIPForgotPassword(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgotPasswordPage());
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
}
