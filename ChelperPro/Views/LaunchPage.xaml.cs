using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class LaunchPage : ContentPage
    {
        void Handle_No(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage( new SignUpPage()));
        }
        void Handle_Yes(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage( new SignInPage()));
        }

        public LaunchPage()
        {
            InitializeComponent();
        }
    }
}
