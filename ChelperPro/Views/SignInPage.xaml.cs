using System;
using System.Collections.Generic;
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
        public List<string> CountryCodes { get; private set; }
        private TwilioHelper thelper = new TwilioHelper();
        public string DefaultCountryCode { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await System.Threading.Tasks.Task.Delay(150);
                DefaultCountryCode = "(US) +1";
                countryCodePicker.SelectedIndex = 1;
                PNumEntry.Focus();
            });
        }
        public SignInPage()
        {
            InitializeComponent();
            CountryCodes = new List<string>();
            CountryCodes.Add("(BR) +55");
            CountryCodes.Add("(US) +1");
            CountryCodes.Add("(RUS) +7");
            CountryCodes.Add("(FR) +33");
            CountryCodes.Add("(UK) +44");
            CountryCodes.Add("(DE) +49");
            CountryCodes.Add("(MX) +52");
            CountryCodes.Add("(JPN) +81");
            CountryCodes.Add("(KR) +82");
            CountryCodes.Add("(CN) +86");
            CountryCodes.Add("(IN) +91");
            CountryCodes.Add("(HK) +852");
            CountryCodes.Add("(TWN) +886");
            NavigationPage.SetHasBackButton(this, false);
            countryCodePicker.SelectedIndex = 1;
            this.BindingContext = this;
        }
        //登入
        private string GetCountryName(string fullcode)
        {
            string[] sArray = fullcode.Split(' ');
            var Flag = "";
            var Code = "";
            if (sArray.Length == 2)
            {
                Flag = sArray[0];
                Code = sArray[1];
            }
            return Code;
        }

        async void Handle_SignIn(object sender, EventArgs e)
        {
            activity.IsEnabled = true;
            activity.IsRunning = true;
            activity.IsVisible = true;
            signInloading.Text = "Connecting...";
            signInloading.TextColor = Color.FromHex("#888888");
            UserAccess userAccess = new UserAccess();
            Uac uac = new Uac();
            uac.ContactNo = GetCountryName(countryCodePicker.SelectedItem.ToString()) + PNumEntry.Text;

            //Internet Connection Check
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                activity.IsEnabled = false;
                activity.IsRunning = false;
                activity.IsVisible = false;
                signInloading.Text = "";
                await DisplayAlert("No Internet", "Try again later!", "OK");
                return;
            }

            //Validation Check
            if (!thelper.IsValidE164(uac.ContactNo, "US"))
            {
                activity.IsEnabled = false;
                activity.IsRunning = false;
                activity.IsVisible = false;
                signInloading.Text = "";
                await DisplayAlert("Not Valid", "Enter a real number and try again!", "OK");
                return;
            }

            //Empty Check
            if (PNumEntry.Text.IsNullOrEmpty())
            {
                activity.IsEnabled = false;
                activity.IsRunning = false;
                activity.IsVisible = false;
                signInloading.Text = "";
                await DisplayAlert("Error", "Try enter your Number and try again!", "OK");
                return;
            }

            //RememberMe = savename.IsToggled;
            if (userAccess.CheckPhoneNoExist(uac.ContactNo))
            {
                Settings.UserId = userAccess.GetUserIDbyNo(uac.ContactNo);
                activity.IsEnabled = false;
                activity.IsRunning = false;
                activity.IsVisible = false;
                signInloading.Text = "";
                await Navigation.PushAsync(new SignInPasswordPage(uac.ContactNo));
            }
            else
            {
                userAccess.TwilioVerifyService(uac.ContactNo);
                activity.IsEnabled = false;
                activity.IsRunning = false;
                activity.IsVisible = false;
                signInloading.Text = "";
                await Navigation.PushAsync(new SignUpVerifyPage(uac.ContactNo));
            }
        }

        //取消按钮 Canceled
        void Handle_Canceled(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
