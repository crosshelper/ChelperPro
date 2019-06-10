using System;
using System.Collections.Generic;
using ChelperPro.Helpers;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class SignUpPage : ContentPage
    {
        UserAccess uAccess = new UserAccess();
        private string Uname = "";
        private string Pwd = "";
        private string Pwdc = "";

        public SignUpPage()
        {
            InitializeComponent();
        }
        //取消按钮 Cancel
        void Handle_Canceled(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        //注册邮箱&密码输入框&再次输入 Sign up email&password&password again input box
        void UnameCompleted(object sender, EventArgs e)
        {
            Uname = ((Entry)sender).Text;
        }
        void PasswordCompleted(object sender, EventArgs e)
        {
            Pwd = ((Entry)sender).Text;
        }
        void PasswordComfirmCompleted(object sender, EventArgs e)
        {
            Pwdc = ((Entry)sender).Text;
        }

        UserAccess uac = new UserAccess();

        //注册按钮 Sign Up
        void Handle_Next(object sender, EventArgs e)
        {
            if(Uname.Length < 5 || Pwd.Length<8 ||Pwd != Pwdc)
            {
               DisplayAlert("No Access", "Try again!", "OK");
               return;
            }
            try
            {
                uac.UserRegister(Uname, "", "", Pwd);
                uac.SetChatID();
                Navigation.PushAsync(new SignUpTwoPage());
            }
            catch (SystemException ex)
            {
              Console.WriteLine(ex);
               return;
            }
        }
    }
}
