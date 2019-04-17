using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class AboutUsPage : ContentPage
    {
        void Handle_Canceled(object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync(false);
        }
        void Handle_Submit(object sender, System.EventArgs e)
        {
            Navigation.PopAsync(false);
        }
        public AboutUsPage()
        {
            InitializeComponent();
        }
    }
}
