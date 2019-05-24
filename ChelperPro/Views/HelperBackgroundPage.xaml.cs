using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class HelperBackgroundPage : ContentPage
    {
        void Handle_Saved(object sender, System.EventArgs e)
        {
            Navigation.PopAsync(false);
        }
        void Handle_Canceled(object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync(false);
        }

        public IList HomeLandItems { get; }
        //Homeland
        public ObservableCollection<HomeLandPickerItem> HomeLandItemsSource { get; } = new ObservableCollection<HomeLandPickerItem>();
        public ObservableCollection<HomeLandPickerItem> HomeLandSelectedItems { get; set; } = new ObservableCollection<HomeLandPickerItem>();


        public HelperBackgroundPage(Models.UserInfo _currentUser)
        {
            InitializeComponent();
            List<string> list = new List<string>
            {
                "Chinese",
                "English",
                "Spanish",
                "Korean",
                "Japanese",
                "French",
                "German",
                "Thai"
            };
            HomeLandItems = list;

            //出生地 pick homeland
            HomeLandItemsSource.Add(new HomeLandPickerItem { Name = "China" });
            HomeLandItemsSource.Add(new HomeLandPickerItem { Name = "United States" });
            HomeLandItemsSource.Add(new HomeLandPickerItem { Name = "France" });
            HomeLandItemsSource.Add(new HomeLandPickerItem { Name = "Germany" });
            HomeLandItemsSource.Add(new HomeLandPickerItem { Name = "Japan" });
            HomeLandItemsSource.Add(new HomeLandPickerItem { Name = "South Korea" });
            HomeLandItemsSource.Add(new HomeLandPickerItem { Name = "Spain" });
            HomeLandItemsSource.Add(new HomeLandPickerItem { Name = "Mexico" });
            HomeLandItemsSource.Add(new HomeLandPickerItem { Name = "Brazil" });
            HomeLandItemsSource.Add(new HomeLandPickerItem { Name = "Thailand" });

            BindingContext = this;
        }
        public class HomeLandPickerItem
        {
            public string Name { get; set; }
        }
    }
}
