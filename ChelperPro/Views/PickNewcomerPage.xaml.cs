using System;
using System.Collections.Generic;
using System.Windows.Input;
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

        readonly TopicInfoHelper tih = new TopicInfoHelper();
        readonly UserInfoHelper uih = new UserInfoHelper();

        public PickNewcomerPage()
        {
            InitializeComponent();
            var _currentZipCodeList = uih.GetMyServiceArea();
            Topics = tih.GetMyTopicList(_currentZipCodeList); //_currentZipCode);
            BindingContext = this;
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            TopicInfoLabel topicInfoLabel = e.Item as TopicInfoLabel;
            ((ListView)sender).SelectedItem = null;
            Navigation.PushAsync(new NewcomerProblemPage(topicInfoLabel));
        }

        void Handle_Toggled(object sender, ToggledEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;
                    RefreshData();
                    IsRefreshing = false;
                });
            }
        }

        private void RefreshData()
        {
            MyListView.ItemsSource = null;
            if (Topics.Count > 0)
            {
                Topics.Clear();
            }
            Topics = tih.GetMyTopicList(uih.GetMyServiceArea());
            MyListView.ItemsSource = Topics;
        }
    }
}
