using System;
using System.Collections.Generic;
using ChelperPro.Helpers;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class ServiceAreaPage : ContentPage
    {
        UserSettingHelper ush = new UserSettingHelper();
        public ServiceAreaPage()
        {
            InitializeComponent();
            List<string> ZipCodeslist = ush.GetZipCodeByID(Settings.UserId);
            if(ZipCodeslist.Count>0)
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

        void Handle_Save(object sender, System.EventArgs e)
        {
            if(!string.IsNullOrEmpty(ZipCode1) && !string.IsNullOrEmpty(ZipCode2) && !string.IsNullOrEmpty(ZipCode3))
            ush.UpdateZipCode(ZipCode1,ZipCode2,ZipCode3);
            Navigation.PopAsync();

        }
    }
}
