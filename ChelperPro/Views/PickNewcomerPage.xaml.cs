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
            Helpers.Add(new PickNewcomerViewcellItem
            {
                PanelID = 1,
                Name = "Foothill center",
                Language = "Language: Chinese/English",
                Status = "Emergency",
                Location = "8 miles away from me",
                ProblemdDscription = "I love youI love youI love youI love youI love youI love youI love youI love youI love you love youI love youlove youI love you love you I love you love you",
                ImageUrl = "https://s3-us-west-1.amazonaws.com/image.cycbis.com/recommendation/recom001.png"
            });

            Helpers.Add(new PickNewcomerViewcellItem
            {
                PanelID = 2,
                Name = "Foothill center",
                Language = "Language: Chinese/English",
                Status = "Emergency",
                Location = "8 miles away from me",
                ProblemdDscription = "I love youI love youI love youI love youI love youI love youI love youI love youI love you love youI love youlove youI love you love you I love you love you",
                ImageUrl = "https://s3-us-west-1.amazonaws.com/image.cycbis.com/recommendation/recom001.png"
            });
            BindingContext = this;
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new NewcomerProblemPage());
        }
    }
}
