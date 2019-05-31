using System;
using System.Collections;
using System.Collections.Generic;
using ChelperPro.Helpers;
using ChelperPro.Models;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class MySpecialitiesPage : ContentPage
    {
        void Handle_Canceled(object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync(false);
        }
        void Handle_Save(object sender, System.EventArgs e)
        {
            Navigation.PopAsync(false);
        }

        public MySpecialitiesPage()
        {
            InitializeComponent();
            UserInfoHelper uih = new UserInfoHelper();
            var taginfoList = uih.GetMyTagsByID(Settings.UserId);
            SelectSkills.ItemsSource = taginfoList;

            string TagsStr = "";
            foreach (TagInfo tag in taginfoList)
            {
                TagsStr += tag.Pcategory + ", ";
            }
            speLabel.Text = TagsStr;
        }
    }
}
