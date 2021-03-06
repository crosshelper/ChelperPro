﻿using System;
using System.Collections.Generic;
using ChelperPro.ViewModels;
using Xamarin.Forms;

namespace ChelperPro.Views.Partials
{
    public partial class ChatInputBarView : ContentView
    {
        public ChatInputBarView()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
            {
                this.SetBinding(HeightRequestProperty, new Binding("Height", BindingMode.OneWay, null, null, null, chatTextInput));
            }
        }
        public void Handle_Completed(object sender, EventArgs e)
        {
            (this.Parent.Parent.BindingContext as ChatTestViewModel).OnSendCommand.Execute(null);
            chatTextInput.Focus();
            chatTextInput.Text = "";
        }

        public void UnFocusEntry(){
            chatTextInput?.Unfocus();
        }

    }
}
