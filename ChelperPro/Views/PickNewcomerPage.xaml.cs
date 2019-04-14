using System;
using System.Collections.Generic;
using ChelperPro.Models;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class PickNewcomerPage : ContentPage
    {
        public IList<PickNewcomerViewcellItem> PanelContent { get; set; }
        public PickNewcomerPage()
        {
            InitializeComponent();
            PanelContent = new List<PickNewcomerViewcellItem>();
            PanelContent.Add(new PickNewcomerViewcellItem
            {
                PanelID = 1,
                Name = "Baboon",
                Language = "Language: Chinese/English",
                Status = "Emergency",
                Location = "8 miles away from me",
                ImageUrl = ""
            });

            PanelContent.Add(new PickNewcomerViewcellItem
            {
                PanelID = 0,
                Name = "Foothill center",
                Language = "Language: Chinese/English",
                Status = "Emergency",
                Location = "8 miles away from me",
                ProblemdDscription = "I love youI love youI love youI love youI love youI love youI love youI love youI love you love youI love youlove youI love you love you I love you love you",
                ImageUrl = "https://s3-us-west-1.amazonaws.com/image.cycbis.com/recommendation/recom001.png"
            });
            PanelContent.Add(new PickNewcomerViewcellItem
            {
                PanelID = 0,
                Name = "Foothill center",
                Language = "Language: Chinese/English",
                Status = "Emergency",
                Location = "8 miles away from me",
                ProblemdDscription = "I love youI love youI love youI love youI love youI love youI love youI love youI love you love youI love youlove youI love you love you I love you love you",
                ImageUrl = "https://s3-us-west-1.amazonaws.com/image.cycbis.com/recommendation/recom001.png"
            });
            PanelContent.Add(new PickNewcomerViewcellItem
            {
                PanelID = 0,
                Name = "Foothill center",
                Language = "Language: Chinese/English",
                Status = "Emergency",
                Location = "8 miles away from me",
                ProblemdDscription = "I love youI love youI love youI love youI love youI love youI love youI love youI love you love youI love youlove youI love you love you I love you love you",
                ImageUrl = "https://s3-us-west-1.amazonaws.com/image.cycbis.com/recommendation/recom001.png"
            });

            listView.ItemsSource = PanelContent;
            listView.BackgroundColor = Color.Transparent;
            listView.SelectionMode = ListViewSelectionMode.None;
        }
        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            //if (e.SelectedItem == null) return;
            //((ListView)sender).SelectedItem = null;
        }
    }
}
