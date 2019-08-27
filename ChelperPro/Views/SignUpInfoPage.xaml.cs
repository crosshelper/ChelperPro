using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SendBird;
using WebSocketSharp;
using ChelperPro.Helpers;
using ChelperPro.Models;
using System.Threading.Tasks;

namespace ChelperPro.Views
{
    public partial class SignUpInfoPage : ContentPage
    {
        UserAccess uAccess = new UserAccess();
        UserInfoHelper uih = new UserInfoHelper();
        KeyChainHelper kch = new KeyChainHelper();
        UserInfo usr = new UserInfo();
        private string Name = "";
        private string ProfileIcon = "";
        private string Email = "";

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(150);
                FNameEntry.Focus();
            });
        }

        public SignUpInfoPage(string contactNo, string pwd)
        {
            _currentNo = contactNo;
            _currentPassword = pwd;
            InitializeComponent();
        }

        void Handle_Canceled(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private string FName, LName, PLanguage;
        private string _currentNo = "";
        private string _currentPassword = "";

        async void Handle_Next(object sender, EventArgs e)
        {
            //TODO: planguage,address,zipcode,ssn,not null or empty
            if (FNameEntry.Text.IsNullOrEmpty() && LNameEntry.Text.IsNullOrEmpty())
            {
                await DisplayAlert("Notice", "Please fill all required information box.", "OK");
            }
            else
            {
                FName = FNameEntry.Text;
                LName = LNameEntry.Text;
                Email = EmailEntry.Text;
                PLanguage = FlanPicker.SelectedItem.ToString();
                //Address = AddressEntry.Text + cityEntry.Text + stateEntry.Text;
                //ZipCode = zipEntry.Text;
                //HelperSSN = SocialEntry.Text;

                if (Email.IsNullOrEmpty())
                    Email = "cycbis@cycbis.com";
                uih.UpdateUserRealNameEmail(FName, LName, Email, PLanguage);
                //uih.UpdateHelperSSN(HelperSSN);
                uAccess.SetChatID();
                //uAccess.UpdateEmailNo(Email, ContactNo);
                //Settings.IsLogin = uAccess.VerifyUser(Uname, Pwd);
                //DisplayAlert("Congrats!", "You Have Done Sign Up, Sign In right now", "OK");
                //
                //kch.SavetoSecureStorage("token_of_" + _currentNo, _currentPassword);
                //signInloading.Text = "Sign In Succeeded, Data Loading...";
                //signInloading.TextColor = Color.FromHex("#555555");
                //Settings.UserId = uAccess.CurrentUid.ToString();
                //usr = uAccess.GetUserInfo(Convert.ToInt32(Settings.UserId));// uAccess.CurrentUid);
                //Settings.ChatID = usr.ChatID;
                //Name = usr.FirstName + " " + usr.LastName;
                //ProfileIcon = usr.Icon;
                //ChatServerConnect();
                //await Task.Delay(3000);
                //Settings.IsLogin = true;
                await Navigation.PushAsync(new SignUpSkillPage());
            }
        }
    }
}