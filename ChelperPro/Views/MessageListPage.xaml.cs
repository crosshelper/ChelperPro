﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using ChelperPro.Helpers;
using ChelperPro.Models;
using ChelperPro.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChelperPro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessageListPage : ContentPage
    {
        MessageListViewModel vm;

        public delegate void GroupHandler(string foo);
        public MessageListPage()
        {
            InitializeComponent();
            BindingContext = vm = new MessageListViewModel();
            vm.Navigation = Navigation;
        }

        private void LstView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var user = (UserInfo)e.Item;
            List<string> users = new List<string>() {
                Settings.ChatID,
                user.ChatID
            };
            ((ListView)sender).SelectedItem = null;
            vm.ConnectToChannel(user, users);
        }
        
    }
}
