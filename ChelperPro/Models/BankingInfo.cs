using System;
namespace ChelperPro.Models
{
    public class BankingInfo
    {
        public string PaymentID { get; set; }
        public string AccountNo { get; set; }
        public string CName { get; set; }
        public DateTime ExDate { get; set; }
        public string CVV { get; set; }
        public string Zipcode { get; set; }
        public string Uid { get; internal set; }

        public BankingInfo()
        {
        }
    }
}
