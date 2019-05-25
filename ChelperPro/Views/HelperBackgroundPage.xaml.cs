using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ChelperPro.Helpers;
using ChelperPro.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChelperPro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelperBackgroundPage : ContentPage
    {
        void Handle_Saved(object sender, System.EventArgs e)
        {
            _usr.FLanguage = FLanguage;
            _usr.SLanguage = SLanguage;
            uih.UpdateUserInfo(_usr);
            Navigation.PopAsync(false);
        }
        void Handle_Canceled(object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync(false);
        }

        UserInfoHelper uih = new UserInfoHelper();
        public IList LanguageItems { get; }

        public string FLanguage { get; set; }
        public string SLanguage { get; set; }

        UserInfo _usr;

        public HelperBackgroundPage(UserInfo _currentUser)
        {
            InitializeComponent();
            _usr = _currentUser;

            FLanguage = _usr.FLanguage;
            SLanguage = _usr.SLanguage;

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
            LanguageItems = list;

            BindingContext = this;
        }
        public class HomeLandPickerItem
        {
            public string Name { get; set; }
        }
    }
}
