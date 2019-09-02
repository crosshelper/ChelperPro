using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChelperPro.Helpers;
using WebSocketSharp;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class SignUpAddPage : ContentPage
    {
        public SignUpAddPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(150);
                if (userAccess.IsSSNExist())
                {
                    SocialEntry.IsEnabled = false;
                }
            });
        }

        UserAccess userAccess = new UserAccess();

        void Handle_Create(object sender, EventArgs e)
        {
            UserInfoHelper uih = new UserInfoHelper();
            
            if (userAccess.IsSSNExist())
            {
                Navigation.PushAsync(new SignUpSkillPage());
            }
            else
            {
                if (SocialEntry.Text.IsNullOrEmpty())
                {
                    uih.CreateHelperSSN("000000000");
                    Navigation.PushAsync(new SignUpSkillPage());
                }
                else
                {
                    uih.CreateHelperSSN(SocialEntry.Text);
                    Navigation.PushAsync(new SignUpSkillPage());
                }
                
            }
            //Navigation.PushAsync(new SignUpSkillPage());
        }
    }
}
