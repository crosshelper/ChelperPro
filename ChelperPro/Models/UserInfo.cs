using System;
namespace ChelperPro.Models
{
    public class UserInfo
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ChatID { get; set; }
        public string FLanguage { get; set; }
        public string SLanguage { get; set; }
        public string PaymentID { get; set; }
        public string Icon { get; set; }
        public string FENo { get; set; }
        public string SENo { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string Homeland { get; internal set; }
        public string Email { get; internal set; }
        public string ZipCode { get; internal set; }

        public UserInfo()
        {
        }
    }
}
