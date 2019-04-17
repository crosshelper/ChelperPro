using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class BankPage : ContentPage
    {
        void Handle_Canceled(object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync(false);
        }
        void Handle_AddPaymentMethod(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AddBankPage());
        }
        void Handle_EditPaymentMethod(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new EditBankPage());
        }
        public BankPage()
        {
            InitializeComponent();
        }
    }
}
