using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class LegalPage : ContentPage
    {
        public LegalPage()
        {
            InitializeComponent();
        }
        void Handle_Terms(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.cycbis.com/#/terms"));
        }
        void Handle_PrivacyPolicy(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.cycbis.com/#/privacy"));
        }
    }
}
