using System;
using System.Collections.Generic;
using ChelperPro.Models;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class HistoryPage : ContentPage
    {
        public IList<HistorypageViewcellItem> Project { get; set; }
        public HistoryPage()
        {
            InitializeComponent();
            Project = new List<HistorypageViewcellItem>();
            Project.Add(new HistorypageViewcellItem
            {
                PanelID = 1,
                Rating = 55555,
                Language = "Chinese/English",
                Emergency = "Emergency",
                Date = "09/20/2018",
                Description = "I lost my langage at Pairs Charles de Gaulle Airport.But I can not speak French, and I dont  know how to find it.",
                Status = "Open",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
            });

            Project.Add(new HistorypageViewcellItem
            {
                PanelID = 4124,
                Rating = 53251555,
                Language = "Chinese/English",
                Emergency = "Emergency",
                Date = "09/20/2018",
                Description = "I lost my langage at Pairs Charles de Gaulle Airport.But I can not speak French, and I dont  know how to find it.",
                Status = "Open",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
            });
            Project.Add(new HistorypageViewcellItem
            {
                PanelID = 23,
                Rating = 551231555,
                Language = "Chinese/English",
                Emergency = "Emergency",
                Date = "09/20/2018",
                Description = "I lost my langage at Pairs Charles de Gaulle Airport.But I can not speak French, and I dont  know how to find it.",
                Status = "Open",
                ImageUrl = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
            });
            currentList.ItemsSource = Project;
            currentTab.Content = currentList;

            pastList.ItemsSource = Project;
            pastTab.Content = pastList;
        }
        void Handle_CurrentItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            //Navigation.PushAsync(new PickHelperPage());
            //((ListView)sender).SelectedItem = null;
        }
        void Handle_PastItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            //Navigation.PushAsync(new PastHistoryDetailPage());
            //((ListView)sender).SelectedItem = null;
        }
    }
}
