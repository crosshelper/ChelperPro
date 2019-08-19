using System;
using System.Collections.Generic;

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
        public SignInPasswordPage()
        {
            InitializeComponent();
        }
        void Handle_Next(object sender, EventArgs e)
        {

            if (pwdEntry.Text == null)
            {
                DisplayAlert("Notice", "Please enter your password and make sure it's least 8 characters. ", "OK");

            }
            else
            {

                Navigation.PushAsync(new SignUpTwoPage());
            }
        }
        void Handle_forgotPwd(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
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
