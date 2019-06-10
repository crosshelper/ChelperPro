using System;
using System.Collections.Generic;
using ChelperPro.Helpers;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class BioPage : ContentPage
    {
        UserInfoHelper uih = new UserInfoHelper();

        public BioPage()
        {
            InitializeComponent();
            BioBox.Text = uih.GetBioByID(Settings.UserId);
        }

        void Handle_Save(object sender, System.EventArgs e)
        {
            uih.UpdateHelperBio(BioBox.Text);
        }
    }
}
