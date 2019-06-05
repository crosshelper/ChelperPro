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
        void Handle_Save(object sender, System.EventArgs e)
        {
            uih.UpdateMySpecialities(BioBox.Text, SelectSkills.Items);
            Navigation.PopAsync(false);
        }

        UserInfoHelper uih = new UserInfoHelper();

        public MySpecialitiesPage()
        {
            InitializeComponent();
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
