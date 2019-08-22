using System;
using System.Collections.Generic;
using ChelperPro.Helpers;
using ChelperPro.Models;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class PwdForgotResetPage : ContentPage
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
        UserInfoHelper uih = new UserInfoHelper();
        private string ContactNo = "";
        private string Pwd = "";

        public PwdForgotResetPage(string _contactNo)
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
                    Uac uac = new Uac();
                    uac.ContactNo = ContactNo;
                    uac.UserID = Settings.UserId;
                    uac.Pwd = Pwd;
                    uih.UpdateUac(uac);

                    Navigation.PushAsync(new SignInPage());
                }
                catch (SystemException ex)
                {
                    Console.WriteLine(ex);
                    return;
                }
            }
        }
    }
}
