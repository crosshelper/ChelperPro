using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class TransferBankPage : ContentPage
    {
        void Handle_Canceled(object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync(false);
        }
        void Handle_EditBankInfo(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new EditBankPage());
        }
        void Handle_TransferToBank(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new TransferSuccessPage());
        }
        public TransferBankPage()
        {
            InitializeComponent();
        }
    }
}
