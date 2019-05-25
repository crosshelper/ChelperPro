using System;
using System.Collections.Generic;
using ChelperPro.Helpers;
using ChelperPro.Models;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class TrustedContactsPage : ContentPage
    {
        void Handle_Canceled(object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync(false);
        }
        void Handle_Saved(object sender, System.EventArgs e)
        {
            _usr.FENo = FENo;
            _usr.SENo = SENo;
            uih.UpdateUserInfo(_usr);
            Navigation.PopAsync(false);
        }

        UserInfoHelper uih = new UserInfoHelper();
        public string FENo { get; set; }
        public string SENo { get; set; }
        UserInfo _usr;

        public TrustedContactsPage(UserInfo _currentUser)
        {
            InitializeComponent();
            _usr = _currentUser;
            FENo = _currentUser.FENo;
            SENo = _currentUser.SENo;
            BindingContext = this;
        }
    }
}
