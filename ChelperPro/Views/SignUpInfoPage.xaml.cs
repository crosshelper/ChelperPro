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
                if (uih.UInfoIDIsNullOrEmpty())
                {
                    var uinfo = uih.GetUserInfoByID(Settings.UserId);
                    FNameEntry.Text=uinfo.FirstName;
                    LNameEntry.Text=uinfo.LastName;
                    EmailEntry.Text=uinfo.Email;
                    FlanPicker.SelectedItem=uinfo.FLanguage;
                    SlanPicker.SelectedItem = uinfo.SLanguage;
                }
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
                if (!uih.UInfoIDIsNullOrEmpty())
                {
                    await Navigation.PushAsync(new SignUpAddPage());
                }
                else
                {
                    FName = FNameEntry.Text;
                    LName = LNameEntry.Text;
                    Email = EmailEntry.Text;
                    PLanguage = FlanPicker.SelectedItem.ToString();
                    if (Email.IsNullOrEmpty())
                        Email = "cycbis@cycbis.com";
                    uih.CreateUserInfo(FName, LName, Email, PLanguage);
                    //uih.UpdateHelperSSN(HelperSSN);
                    uAccess.SetChatID();
                    await Navigation.PushAsync(new SignUpAddPage());
                }
            }
        }
    }
}