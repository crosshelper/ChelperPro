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

        public ProfilePage()
        {
            InitializeComponent();
        }
    }
}
