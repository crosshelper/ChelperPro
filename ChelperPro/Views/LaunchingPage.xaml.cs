using System;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class LaunchingPage : ContentPage
    {
        void Handle_GetStarted(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new Xamarin.Forms.NavigationPage(new SignInPage()));
        }

        public LaunchingPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
    }
}
