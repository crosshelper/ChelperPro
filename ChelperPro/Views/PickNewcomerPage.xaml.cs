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

        public void OnMore(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Report to us","Please send email to 'supports@cycbis.com' with the screenshot of the problem, thanks!","Got it"); //"More Context Action", mi.CommandParameter + " more context action", "OK");
        }
        public List<TopicInfoLabel> Topics { get; private set; }
        TopicInfoHelper tih = new TopicInfoHelper();
        UserInfoHelper uih = new UserInfoHelper();

        public PickNewcomerPage()
        {
            InitializeComponent();
            var _currentZipCodeList = uih.GetMyServiceArea();
            Topics = tih.GetMyTopicList(_currentZipCodeList); //_currentZipCode);
            BindingContext = this;
        }

        void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            TopicInfoLabel topicInfoLabel = e.SelectedItem as TopicInfoLabel;
            Navigation.PushAsync(new NewcomerProblemPage(topicInfoLabel));
        }

        void Handle_Toggled(object sender, ToggledEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
