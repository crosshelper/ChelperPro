using System;
using System.Collections.Generic;
using ChelperPro.Helpers;
using ChelperPro.Models;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class BankPage : ContentPage
    {
        void Handle_Canceled(object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync(false);
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            DisplayAlert("Sorry,current unavailable", "You can feel free to chat and earn money, the online payment option will be available soon.", "OK");
            //BankingInfo ptmp = e.Item as BankingInfo;
            //((ListView)sender).SelectedItem = null;
            //Navigation.PushAsync(new EditBankPage(ptmp));
        }

        BankingInfo bankInfo = new BankingInfo();
        MyWalletHelper wmh = new MyWalletHelper();

        public BankPage()
        {
            InitializeComponent();

            bankInfo = wmh.GetBankingInfoByID(Settings.UserId);

            //if(bankInfo.CVV != "")
                //BankCell.Title = "Add your Banking Account";
            //else
                //BankCell.Title = bankInfo.AccountNo;
            BindingContext = this;
        }
    }
}
