using System;
using System.Collections.Generic;
using ChelperPro.Models;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class PickNewcomerPage : ContentPage
    {
        void Handle_location(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CurrentLocationPage());
        }

        public IList<PickNewcomerViewcellItem> Helpers { get; private set; }
        public PickNewcomerPage()
        {
            InitializeComponent();
            Helpers = new List<PickNewcomerViewcellItem>();

            BindingContext = this;
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new NewcomerProblemPage());
        }
    }
}
