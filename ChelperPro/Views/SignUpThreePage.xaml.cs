using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class SignUpThreePage : ContentPage
    {
        public SignUpThreePage()
        {
            InitializeComponent();
        }
        void Handle_OK(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new LaunchPage();
        }
    }
}
