using System;
using System.Threading.Tasks;
using SendBird;
using Xamarin.Forms;
using Xamarin.Essentials;
using ChelperPro.Helpers;
using ChelperPro.Models;

namespace ChelperPro.Views
{
    public class MySplashScreen : ContentPage
    {
        public MySplashScreen()
        {
            Content = new StackLayout
            {
                Children = {
                    new ActivityIndicator()
                    {
                        Color = Color.FromHex("#999999"),
                        IsEnabled = true,
                        IsRunning = true,
                        IsVisible = true,
                        WidthRequest = 40,
                        BackgroundColor= Color.Transparent,
                       // HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    },
                    new Label { Text = "Data Loading......",
                        TextColor=Color.FromHex("#999999"),
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 26,
                        Margin = new Thickness (20),
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand }
                }
            };
            CheckForAutoLogin();
        }

        private string Name = "";
        private string ProfileIcon = "";
        readonly KeyChainHelper kch = new KeyChainHelper();

        public bool RememberMe
        {
            get => Preferences.Get(nameof(RememberMe), false);
            set
            {
                Preferences.Set(nameof(RememberMe), value);
                if (!value)
                {
                    Preferences.Set(nameof(Username), string.Empty);
                }
                OnPropertyChanged(nameof(RememberMe));
            }
        }

        string username = Preferences.Get(nameof(Username), string.Empty);
        public string Username
        {
            get => username;
            set
            {
                username = value;
                if (RememberMe)
                {
                    Preferences.Set(nameof(Username), value);
                }
                OnPropertyChanged(nameof(RememberMe));
            }
        }

        private async void CheckForAutoLogin()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("No Internet", "Try again later!", "OK");
                Application.Current.MainPage = new LaunchPage();
            }

            if (Username != string.Empty)
            {
                //Redirect to you desired page
                UserAccess userAccess = new UserAccess();
                UserInfo usr = new UserInfo();
                var pwd = kch.GetFromSecureStorage("token_of_" + Username);
                if (userAccess.VerifyUser(Username, pwd.Result))
                {
                    Settings.UserId = userAccess.CurrentUid.ToString();
                    usr = userAccess.GetUserInfo(userAccess.CurrentUid);
                    Settings.ChatID = usr.ChatID;
                    Name = usr.FirstName + " " + usr.LastName;
                    ProfileIcon = usr.Icon;
                    ChatServerConnect();
                    await Task.Delay(3000);
                    //await Navigation.PushModalAsync(new MyTabbedPage());
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    await Navigation.PushModalAsync(new LaunchPage());
                }
            }
            else
            {
                await Navigation.PushModalAsync(new LaunchPage());
            }
        }

        private void ChatServerConnect()
        {
            SendBirdClient.Connect(Settings.ChatID, (SendBird.User user, SendBirdException e) =>
            {
                if (e != null)
                {
                    return;
                }

                SendBirdClient.UpdateCurrentUserInfo(Name, ProfileIcon, (SendBirdException e1) =>
                {
                    if (e1 != null)
                    {
                        return;
                    }
                });
                SendBirdClient.RegisterAPNSPushTokenForCurrentUser(SendBirdClient.GetPendingPushToken(), (SendBirdClient.PushTokenRegistrationStatus status, SendBirdException e1) =>
                {
                    if (e1 != null)
                    {
                        // Error.
                        return;
                    }

                    if (status == SendBirdClient.PushTokenRegistrationStatus.PENDING)
                    {
                        // Try registration after connection is established.
                    }
                });
            });
            Settings.IsLogin = true;
        }
    }
}

