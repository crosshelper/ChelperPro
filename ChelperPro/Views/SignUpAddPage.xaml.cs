using System;
using System.Collections.Generic;
using ChelperPro.Helpers;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class SignUpAddPage : ContentPage
    {
        public SignUpAddPage()
        {
            InitializeComponent();
        }
        void Handle_Create(object sender, EventArgs e)
        {
            UserInfoHelper uih = new UserInfoHelper();
            //uih.UpdateHelperSSN(SocialEntry.Text);
            uih.CreateHelperSSN(SocialEntry.Text);
            Navigation.PopToRootAsync();
            //Navigation.PushAsync(new SignUpSkillPage());
        }
    }
}
