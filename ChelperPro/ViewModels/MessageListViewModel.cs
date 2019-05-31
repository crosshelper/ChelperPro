using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ChelperPro.Helpers;
using ChelperPro.Models;
using ChelperPro.Views;
using SendBird;
using Xamarin.Forms;

namespace ChelperPro.ViewModels
{
    public class MessageListViewModel : ViewModelBase
    {
        #region Properties
        public ObservableCollection<UserInfo> Users { get; set; } = new ObservableCollection<UserInfo>();
        public INavigation Navigation;
        #endregion
        public MessageListViewModel()
        {
            GroupChannelListQuery mQuery = GroupChannel.CreateMyGroupChannelListQuery();
            mQuery.IncludeEmpty = true;
            mQuery.Next((List<GroupChannel> list, SendBirdException e) => {
                if (e != null)
                {
                    // Error.
                    return;
                }
                foreach (GroupChannel channel in list)
                {
                    foreach (User user in channel.Members)
                    {
                        if (user.UserId != Settings.ChatID)
                        {
                            Users.Add(new UserInfo
                            {
                                ChatID = user.UserId,
                                FirstName = user.Nickname,
                                Icon = user.ProfileUrl
                            });
                        }
                    }
                }
            });
            Users.Add(new UserInfo { ChatID = "cycbis_004", FirstName = "Thomas Wong" });
        }

        public async void ConnectToChannel(Models.UserInfo user, List<string> users)
        {
            GroupChannel group = null;
            IsBusy = true;

            GroupChannel.CreateChannelWithUserIds(users, true, (GroupChannel groupChannel, SendBirdException e) => {
                if (e != null)
                {
                    // Error..
                    return;
                }
                group = groupChannel;
            });
            await Task.Delay(1000);
            IsBusy = false;
            await Navigation.PushModalAsync(new NavigationPage(new ChatPage(user, group)));
        }
    }
}
