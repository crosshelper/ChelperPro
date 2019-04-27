using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class ProfilePage : ContentPage
    {
        void Handle_Account(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AccountPage());
        }
        void Handle_HelperBackGround(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new HelperBackgroundPage());
        }
        void Handle_Bank(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new BankPage());
        }
        void Handle_TrustedContacts(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new TrustedContactsPage());
        }
        void Handle_PrivacyPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PrivacyPage());
        }
        void Handle_AgreementPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AgreementPage());
        }
        void Handle_AboutUsPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AboutUsPage());
        }
        void Handle_MySpecicalPage(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MySpecialitiesPage());
        }

        void Handle_SignOutPage(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new SignInPage();
        }
        public ProfilePage()
        {
            InitializeComponent();
        }
    }
}
