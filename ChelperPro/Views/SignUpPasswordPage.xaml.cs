using System;
using System.Collections.Generic;
using ChelperPro.Helpers;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class SignUpPasswordPage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await System.Threading.Tasks.Task.Delay(150);
                PwdEntry.Focus();
            });
        }

        UserAccess uac = new UserAccess();
        UserInfoHelper uih = new UserInfoHelper();
        private string ContactNo = "";
        private string Pwd = "";

        public SignUpPasswordPage(string _contactNo)
        {
            ContactNo = _contactNo;
            InitializeComponent();
        }
        void Handle_Next(object sender, EventArgs e)
        {
            if (PwdEntry.Text == null)
            {
                DisplayAlert("Notice", "Please enter your password and make sure it's least 8 characters. ", "OK");
            }
            else
            {
                Pwd = PwdEntry.Text;
                if (Pwd.Length < 8)
                {
                    DisplayAlert("No Access", "Please enter your password and make sure it's least 8 characters!", "OK");
                    return;
                }
                try
                {
                    uac.UserRegister(ContactNo, Pwd);
                    Settings.UserId = uac.GetUserIDbyNo(ContactNo);
                    //uih.UpdateUserRealNameEmail(ContactNo, "Cycbis", "cycbis@cycbis.com");

                    Navigation.PushAsync(new SignUpInfoPage(ContactNo, Pwd));
                }
                catch (SystemException ex)
                {
                    Console.WriteLine(ex);
                    return;
                }
                //Navigation.PushAsync(new SignUpTwoPage(Uname,Pwd)); */
                //Navigation.PushAsync(new SignUpInfoPage());
            }
        }
        void Handle_forgotPwd(object sender, System.EventArgs e)
        {
            //Navigation.PushAsync(new ResetPage());
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
