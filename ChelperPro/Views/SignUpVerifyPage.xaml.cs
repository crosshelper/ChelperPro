using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Twilio;
using Twilio.Rest.Verify.V2.Service;
using ChelperPro.Helpers;

namespace ChelperPro.Views
{
    public partial class SignUpVerifyPage : ContentPage
    {
        private string _contactNo = "";
        private UserAccess userAccess = new UserAccess();

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await System.Threading.Tasks.Task.Delay(150);
                CodeEntry.Focus();
            });
        }

        public SignUpVerifyPage(string contactNo)
        {
            _contactNo = contactNo;
            InitializeComponent();
        }

        //取消按钮 Cancel
        void Handle_Canceled(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        //注册按钮 Sign Up
        void Handle_Next(object sender, EventArgs e)
        {

            const string accountSid = "AC86ac48ee4086ad028d5c75b60bc28d12";
            const string authToken = "88bde17df05d1be8d26239c2915f0960";

            TwilioClient.Init(accountSid, authToken);

            var verificationCheck = VerificationCheckResource.Create(
                to: _contactNo,//"+14159373912",
                code: CodeEntry.Text, //"123456",
                pathServiceSid: "VA0f7950bc53a8d465b01ecff8f8c96f1c"
            );

            Console.WriteLine(verificationCheck.Status);

            if (CodeEntry.Text == null || verificationCheck.Status != "approved")
            {
                DisplayAlert("Notice", "Please check your text code and make sure it's correct. ", "OK");
            }
            else
            {
                Navigation.PushAsync(new SignUpPasswordPage(_contactNo));
            }
        }
        void Handle_CodeAgain(object sender, System.EventArgs e)
        {
            //TODO: Timer
            userAccess.TwilioVerifyService(_contactNo);
            var time = 45;
            TmcodeAgain.Text = "Resent code in " + time + " second";
            TmcodeAgain.TextColor = Color.FromHex("#888888");
            //this.IsEnabled = false;

        }
    }
}
