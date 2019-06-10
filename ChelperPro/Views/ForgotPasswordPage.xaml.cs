using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();

        }
        void Handle_Canceled(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        void Handle_Reset(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new LaunchPage();
        }
        void Handle_Sent(object sender, System.EventArgs e)
        {
            //Navigation.PopAsync(false);
        }
    }
}
