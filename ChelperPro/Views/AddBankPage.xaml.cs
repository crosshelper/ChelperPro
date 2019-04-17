using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class AddBankPage : ContentPage
    {
        void Handle_Canceled(object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync(false);
        }
        void Handle_AddPyament(object sender, System.EventArgs e)
        {
            Navigation.PopAsync(false);
        }
        public AddBankPage()
        {
            InitializeComponent();
        }
    }
}
