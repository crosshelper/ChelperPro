using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ChelperPro.Helpers;
using ChelperPro.Models;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class MySpecialitiesPage : ContentPage
    {
        void Handle_Save(object sender, System.EventArgs e)
        {
            if(SelectSkills.SelectedItems.Count == 3)
            {
                //uih.UpdateMySpecialities(BioBox.Text, (IList<TagInfo>)SelectSkills.SelectedItems);
                Navigation.PopAsync(false);
            }
            else
            {
                DisplayAlert("Not accessable!", "Not a valid selection, please try again!", "OK");
            }
        }

        UserInfoHelper uih = new UserInfoHelper();
        TagsHelper th = new TagsHelper();
        public MySpecialitiesPage()
        {
            InitializeComponent();
            var mytaginfoList = uih.GetMyTagsByID(Settings.UserId);
            var alltagslist = th.GetTagList();
            string TagsStr = "";
            foreach (TagInfo tag in mytaginfoList)
            {
                TagsStr += tag.Pcategory + ", ";
            }

            SelectSkills.ItemsSource = alltagslist;
            SelectSkills.SelectedItems = mytaginfoList;
            SelectSkills.DisplayMember = "Pcategory";
            SelectSkills.Title = "My Skills";
            SelectSkills.UseAutoValueText = true;
            SelectSkills.KeepSelectedUntilBack = true;
            SelectSkills.SelectedCommand = new Command(RefreshTag);
            //speLabel.Text = TagsStr;
        }

        private void RefreshTag()
        {
            //speLabel.Text = "";
            var mytaginfoList = uih.GetMyTagsByID(Settings.UserId);
            string TagsStr = "";
            foreach (TagInfo tag in mytaginfoList)
            {
                TagsStr += tag.Pcategory + ", ";
            }
            //speLabel.Text = TagsStr;
        }


    }
}
