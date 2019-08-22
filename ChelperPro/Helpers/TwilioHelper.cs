using System;
using System.Text.RegularExpressions;
using PhoneNumbers;
using Xamarin.Forms;

namespace ChelperPro.Helpers
{
    public class TwilioHelper
    {
        public bool IsValidPhone(string Phone)
        {
            try
            {
                if (string.IsNullOrEmpty(Phone))
                    return false;
                string pattern = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
                var r = new Regex(pattern, RegexOptions.IgnoreCase);
                return r.IsMatch(Phone);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool IsValidE164(string Phone, string CountryCode)
        {
            try
            {
                if (string.IsNullOrEmpty(Phone))
                    return false;
                var phoneUtil = PhoneNumberUtil.GetInstance();
                var numberProto = phoneUtil.Parse(Phone, CountryCode);
                var formattedPhone = phoneUtil.Format(numberProto, PhoneNumberFormat.INTERNATIONAL);
                var r = phoneUtil.IsValidNumber(numberProto);
                return r;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}