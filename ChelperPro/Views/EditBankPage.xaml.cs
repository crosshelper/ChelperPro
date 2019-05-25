using System;
using System.Collections.Generic;
using ChelperPro.Helpers;
using ChelperPro.Models;
using Xamarin.Forms;

namespace ChelperPro.Views
{
    public partial class EditBankPage : ContentPage
    {
        void Handle_Canceled(object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync(false);
        }

        public EditBankPage(BankingInfo ptmp)
        {
            _pinfo = ptmp;
            CardNumber = ptmp.AccountNo;
            ExDate = ptmp.ExDate;
            CName = ptmp.CName;
            CVV = ptmp.CVV;
            Zip = ptmp.Zipcode;
            InitializeComponent();
            BindingContext = this;
        }

        public EditBankPage()
        {
        }

        void Handle_SavePyament(object sender, System.EventArgs e)
        {
            _pinfo.AccountNo = CardNumber;
            _pinfo.CName = CName;
            _pinfo.ExDate = ExDate.Date;
            _pinfo.CVV = CVV;
            _pinfo.Zipcode = Zip;
            mwh.UpdateBankingInfo(_pinfo);
            Navigation.PopAsync(false);
        }

        public string CardNumber { get; set; }
        public DateTime ExDate { get; set; }
        public string CName { get; set; }
        public string CVV { get; set; }
        public string Zip { get; set; }

        BankingInfo _pinfo = new BankingInfo();
        MyWalletHelper mwh = new MyWalletHelper();

    }
}
