using System.Collections.Generic;
using System.Threading.Tasks;
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
                if (uih.IsTagExist())
                {
                    uih.UpdateHelperTags((IList<TagInfo>)SelectSkills.SelectedItems);
                }
                else
                {
                    uih.CreateHelperTags((IList<TagInfo>)SelectSkills.SelectedItems);
                }
                Navigation.PopAsync(false);
            }
            else
            {
                DisplayAlert("Not accessable!", "Not a valid selection, please try again!", "OK");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(150);
                MytaginfoList = uih.GetMyTagsByID(Settings.UserId);
                SelectSkills.SelectedItems = MytaginfoList;
            });
        }

        UserInfoHelper uih = new UserInfoHelper();
        TagsHelper th = new TagsHelper();
        public List<TagInfo> Alltagslist = new List<TagInfo>();
        public List<TagInfo> MytaginfoList = new List<TagInfo>();

        public MySpecialitiesPage()
        {
            InitializeComponent();

            //MytaginfoList = uih.GetMyTagsByID(Settings.UserId);
            Alltagslist = th.GetTagList();
            SelectSkills.ItemsSource = Alltagslist;
            //SelectSkills.SelectedItems = MytaginfoList;
            SelectSkills.DisplayMember = "Pcategory";
            SelectSkills.Title = "My Skills";
            SelectSkills.UseAutoValueText = true;
            SelectSkills.KeepSelectedUntilBack = true;
            SelectSkills.SelectedCommand = new Command(RefreshTag);
            BindingContext = this;
        }

        private void RefreshTag()
        {
            var mytaginfoList = uih.GetMyTagsByID(Settings.UserId);
            string TagsStr = "";
            foreach (TagInfo tag in mytaginfoList)
            {
                TagsStr += tag.Pcategory + ", ";
            }
        }
    }
}
