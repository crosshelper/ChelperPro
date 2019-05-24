using System;
using System.Collections.Generic;
using ChelperPro.Helpers;
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

        public List<TopicInfoLabel> Topics { get; private set; }
        TopicInfoHelper tih = new TopicInfoHelper();

        public PickNewcomerPage()
        {
            InitializeComponent();
            Topics = tih.GetMyTopicList("95131");
            BindingContext = this;
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new NewcomerProblemPage());
        }
    }
}
