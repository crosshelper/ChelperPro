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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await System.Threading.Tasks.Task.Delay(150);
                PNumEntry.Focus();
            });
        }
        public SignInPage()
        {
            InitializeComponent();
            CountryCodes = new List<string>();
            CountryCodes.Add("+55");
            CountryCodes.Add("(US) +1");
            CountryCodes.Add("+7");
            CountryCodes.Add("+33");
            CountryCodes.Add("+44");
            CountryCodes.Add("+49");
            CountryCodes.Add("+52");
            CountryCodes.Add("+81");
            CountryCodes.Add("+82");
            CountryCodes.Add("+86");
            CountryCodes.Add("+91");
            CountryCodes.Add("+852");
            CountryCodes.Add("+886");
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
