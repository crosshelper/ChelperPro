using System;
using System.Collections.Generic;
using ChelperPro.Helpers;
using ChelperPro.Models;
using WebSocketSharp;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class SignUpSkillPage : ContentPage
    {
        public List<TagInfo> TypeProblems { get; private set; } = new List<TagInfo>();
        List<TagInfo> InsertTags = new List<TagInfo>();
        UserSettingHelper ush = new UserSettingHelper();
        public SignUpSkillPage()
        {
            InitializeComponent();
            TypeProblems = th.GetTagList();
            List<string> ZipCodeslist = ush.GetZipCodeByID(Settings.UserId);
            if (ZipCodeslist.Count > 0)
            {
                ZipCode1 = ZipCodeslist[0];
                ZipCode2 = ZipCodeslist[1];
                ZipCode3 = ZipCodeslist[2];
            }
            BindingContext = this;
        }

        public string ZipCode1 { get; set; }
        public string ZipCode2 { get; set; }
        public string ZipCode3 { get; set; }

        void Handle_Next(object sender, EventArgs e)
        {
            var temp = new TagInfo() { TagID = 99999, ImageUrl = "temp", Pcategory = "General" };
            if (FSkillType.SelectedItem == null)
            {
                DisplayAlert("Opps!","You should pick at least one skill","Ok");
                return;
            }
            else
            {
                InsertTags.Add((TagInfo)FSkillType.SelectedItem);
                if (SSkillType.SelectedItem == null)
                {
                    InsertTags.Add(temp);
                    if (TSkillType.SelectedItem == null)
                    {
                        InsertTags.Add(temp);
                    }
                    else
                    {
                        InsertTags.Add((TagInfo)TSkillType.SelectedItem);
                    }
                }
                else
                {
                    InsertTags.Add((TagInfo)SSkillType.SelectedItem);
                    if (TSkillType.SelectedItem == null)
                    {
                        InsertTags.Add(temp);
                    }
                    else
                    {
                        InsertTags.Add((TagInfo)TSkillType.SelectedItem);
                    }
                }
            }
            uih.UpdateHelperTags(InsertTags);
            if (!string.IsNullOrEmpty(ZipCode1) && !string.IsNullOrEmpty(ZipCode2) && !string.IsNullOrEmpty(ZipCode3))
                ush.UpdateZipCode(ZipCode1, ZipCode2, ZipCode3);

            DisplayAlert("Congratulations!", "It's time to launch.", "OK");
            Application.Current.MainPage = new LaunchingPage();
            //Navigation.PopToRootAsync();
        }
        TagsHelper th = new TagsHelper();
        UserInfoHelper uih = new UserInfoHelper();
    }
}
