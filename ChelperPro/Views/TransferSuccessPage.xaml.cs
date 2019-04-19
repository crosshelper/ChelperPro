using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class TransferSuccessPage : ContentPage
    {
        void Handle_Done(object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync(false);
        }
        public TransferSuccessPage()
        {
            InitializeComponent();
        }
    }
}
