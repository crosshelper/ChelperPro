using System;
namespace ChelperPro.Models
{
    public class ReceiptInfo
    {
        public double ServiceFee { get; set; }
        public double EqFee { get; set; }
        public double Tax { get; set; }
        public double Surcharge { get; set; }
        public DateTime PaymentDateTime { get; set; }
        public string PaymentName { get; set; }

        public ReceiptInfo()
        {
        }
    }
}
