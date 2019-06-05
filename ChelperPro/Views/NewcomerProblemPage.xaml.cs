﻿using System;
using System.Collections.Generic;
using ChelperPro.Helpers;
using ChelperPro.Models;
using Xamarin.Forms;
using SendBird;
using System.Threading.Tasks;

namespace ChelperPro.Views
{
    public partial class NewcomerProblemPage : ContentPage
    {
        //Top Ring&Menu button
        void NHPPBackButton(object sender, EventArgs e)
        {
            (sender as Button).Text = "Click me again!";
        }
        void NHPPCancelButton(object sender, EventArgs e)
        {
            (sender as Button).Text = "Click me again!";
        }
        UserInfoHelper uih = new UserInfoHelper();
        //Confirm
        void NHPPConfirm(object sender, EventArgs e)
        {
            if (Settings.IsLogin)
            {
                var user = new UserInfo()
                {
                    UserID = uih.GetChatIDByUid(_currentnewcomerlabel.UserID)
                };
                List<string> users = new List<string>() {
                Settings.ChatID,
                uih.GetChatIDByUid(_currentnewcomerlabel.UserID)
                };
                ConnectToChannel(user, users);
            }
        }
        void Handle_Canceled(object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync(false);
        }

        TopicInfoLabel _currentnewcomerlabel = new TopicInfoLabel();
        public string Name { get; set; }
        public string Language { get; set; }
        public string Des { get; set; }
        public string TagType { get; set; }

        public NewcomerProblemPage(TopicInfoLabel topicInfoLabel)
        {
            _currentnewcomerlabel = topicInfoLabel;
            InitializeComponent();
            Name = _currentnewcomerlabel.Name;
            Language = _currentnewcomerlabel.Language;
            Des = _currentnewcomerlabel.Description;
            TagType = _currentnewcomerlabel.TagName;
        }

        async void ConnectToChannel(UserInfo user, List<string> users)
        {
            GroupChannel group = null;
            IsBusy = true;

            GroupChannel.CreateChannelWithUserIds(users, true, (GroupChannel groupChannel, SendBirdException e) => {
                if (e != null)
                {
                    // Error.
                    return;
                }
                group = groupChannel;
            });
            await Task.Delay(3000);
            IsBusy = false;
            await Navigation.PushAsync(new ChatPage(user, group));
        }

    }
}
