﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class EarnPage : ContentPage
    {
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopAsync(false);
        }
        void Handle_TransferToBank(object sender, System.EventArgs e)
        {
            //TODO: Stripe Connect code here

            DisplayAlert("Sorry,current unavailable", "It will be suppported later.", "OK");
            //Navigation.PushAsync(new TransferBankPage());
        }

        public EarnPage()
        {
            InitializeComponent();
        }
    }
}
