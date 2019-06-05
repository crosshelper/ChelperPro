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
            if(SelectSkills.SelectedItems.Count == 3)
            {
                uih.UpdateMySpecialities(BioBox.Text, (IList<TagInfo>)SelectSkills.SelectedItems);
                Navigation.PopAsync(false);
            }
            else
            {
                DisplayAlert("Not accessable!", "Not a valid selection, please try again!", "OK");
            }
        }

        UserInfoHelper uih = new UserInfoHelper();
        TagsHelper th = new TagsHelper();
        //List<TagInfo> MyTagList = new List<TagInfo>();
        public MySpecialitiesPage()
        {
            InitializeComponent();
            var mytaginfoList = uih.GetMyTagsByID(Settings.UserId);
            //MyTagList = mytaginfoList;
            var alltagslist = th.GetTagList();
            //var mytags = new List<string>();
            //var alltags = new List<string>();
            string TagsStr = "";
            //foreach(TagInfo ti in alltagslist)
            //{
                //alltags.Add(ti.Pcategory); 
            //}
            foreach (TagInfo tag in mytaginfoList)
            {
                //mytags.Add(tag.Pcategory);
                TagsStr += tag.Pcategory + ", ";
            }
            SelectSkills.ItemsSource = alltagslist; //alltags;
            SelectSkills.SelectedItems = mytaginfoList;//mytags;
            speLabel.Text = TagsStr;
        }
    }
}
