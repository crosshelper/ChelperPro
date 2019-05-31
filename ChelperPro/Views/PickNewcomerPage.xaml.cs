using System;
using System.Collections.Generic;
using ChelperPro.Helpers;
using ChelperPro.Models;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class PickNewcomerPage : ContentPage
    {
        void Handle_location(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new CurrentLocationPage());
        }
        public string _currentZipCode { get; private set; } = "95131";
        private List<string> _currentZipCodeList { get; set; }
        public List<TopicInfoLabel> Topics { get; private set; }
        TopicInfoHelper tih = new TopicInfoHelper();

        public PickNewcomerPage()
        {
            InitializeComponent();
            Topics = tih.GetMyTopicList(_currentZipCode);
            BindingContext = this;
        }

        void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            TopicInfoLabel topicInfoLabel = e.SelectedItem as TopicInfoLabel;
            Navigation.PushAsync(new NewcomerProblemPage(topicInfoLabel));
        }

        void Handle_Toggled(object sender, ToggledEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
