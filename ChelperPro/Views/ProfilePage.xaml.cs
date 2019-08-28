using System;
using System.Collections.Generic;
using ChelperPro.Helpers;
using ChelperPro.Models;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class ProfilePage : ContentPage
    {
        void Handle_Account(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AccountPage(_currentUser));
        }
        void Handle_HelperBackGround(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new HelperBackgroundPage(_currentUser));
        }
        void Handle_ServiceArea(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ServiceAreaPage());
        }
        void Handle_Bank(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new BankPage());
        }
        void Handle_TrustedContacts(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new TrustedContactsPage(_currentUser));
        }

        void Handle_LegalPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new LegalPage());
        }
        void Handle_AboutUsPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AboutUsPage());
        }
        void Handle_MySpecicalPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MySpecialitiesPage());
        }
        void Handle_BioPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new BioPage());
        }
        void Handle_SignOutPage(object sender, System.EventArgs e)
        {
            Settings.IsLogin = false;
            Application.Current.MainPage = new LaunchingPage();
        }

        private UserInfo _currentUser;
        UserInfoHelper uih = new UserInfoHelper();

        public ProfilePage()
        {
            InitializeComponent();
            _currentUser = uih.GetUserInfoByID(Settings.UserId);
            UserCell.Title = _currentUser.FirstName + " " + _currentUser.LastName;
            UserCell.IconSource = _currentUser.Icon;
        }
    }
}
