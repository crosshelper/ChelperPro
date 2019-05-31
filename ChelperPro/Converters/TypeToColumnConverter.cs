using ChelperPro.Helpers;
using System;
using SendBird;
using Xamarin.Forms;

namespace ChelperPro.Converters
{
    public class TypeToColumnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int column = 0;
            var user = (User)value;
            var userName = Settings.ChatID;
            if (user.UserId != userName)
            {
                column = 0;
            }
            else
            {
                column = 1;
            }
            return column;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
