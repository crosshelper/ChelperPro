using System;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class LaunchPage : ContentPage
    {
        void Handle_Yes(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new Xamarin.Forms.NavigationPage( new SignInPage()));
        }

        public LaunchPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
    }
}
