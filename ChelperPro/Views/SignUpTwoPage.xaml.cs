using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class SignUpTwoPage : ContentPage
    {
        public SignUpTwoPage()
        {
            InitializeComponent();
        }
        void Handle_Canceled(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync(false);
        }
        //返回按钮 Go Back
        void SUPGoBack(object sender, EventArgs e)
        {
            (sender as Button).Text = "Click me again!";
        }
        //注册邮箱&密码输入框&再次输入 Sign up email&password&password again input box
        void SUPEmailCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
        }
        void SUPPasswordCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
        }
        void SUPComfirmPasswordCompleted(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
        }
        //注册按钮 Sign Up
        void Handle_Next(object sender, EventArgs e)
        {
            Navigation.PopAsync(false);
        }
        void SUPSignUpAsHelper(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpTwoPage());
        }
    }
}
