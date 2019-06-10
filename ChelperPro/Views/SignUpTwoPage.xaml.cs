using System;
using System.Collections.Generic;
using ChelperPro.Helpers;
using WebSocketSharp;
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
            Navigation.PopModalAsync();
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

        UserAccess uac = new UserAccess();

        private bool CheckValid()
        {
            bool IsValid = true && !(EmailEntry.Text.IsNullOrEmpty());
            IsValid &= NoEntry.Text.Length >= 10;
            IsValid &= !(FLEntry.Text.IsNullOrEmpty());
            IsValid &= !(SLEntry.Text.IsNullOrEmpty());
            IsValid &= SocialEntry.Text.Length == 9;
            if (AddressEntry.Text.IsNullOrEmpty())
            {
                IsValid = false;
            }
            return IsValid; 
        }

        //注册按钮 Sign Up
        void Handle_CreateAccount(object sender, EventArgs e)
        {
            if (!CheckValid())
            {
                DisplayAlert("No Access", "Try again!", "OK");
                return;
            }
            try
            {
                uac.UpdateHelperInfo(EmailEntry.Text, NoEntry.Text, FLEntry.Text, SLEntry.Text, SocialEntry.Text, AddressEntry.Text);
                //TODO:???PUSH OR Current
                Navigation.PushAsync(new SignUpThreePage());
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex);
                return;
            }

        }
        async void Handle_Upload(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Upload ID", "Cencel", null, "Take photo", "From album");
        }
    }
}
