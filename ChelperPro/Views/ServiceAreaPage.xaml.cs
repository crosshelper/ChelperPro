using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class ServiceAreaPage : ContentPage
    {
        public ServiceAreaPage()
        {
            InitializeComponent();
        }
        void Handle_Save(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
