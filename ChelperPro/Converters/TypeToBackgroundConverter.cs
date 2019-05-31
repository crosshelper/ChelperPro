using System;
using ChelperPro.Helpers;
using SendBird;
using Xamarin.Forms;

namespace ChelperPro.Converters
{
    public class TypeToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int parameterType = int.Parse(parameter.ToString());
            Xamarin.Forms.Color background = Color.Silver;
            var user = (User)value;
            var userName = Settings.ChatID;
            if (user.UserId == userName)
            {
                background = Color.FromHex("#FFFFFF");
                if (parameterType == 0)
                    background = Color.FromHex("#F1F0F0");
            }
            else
            {
                background = Color.FromHex("#F1F0F0");
                if (parameterType == 0)
                    background = Color.FromHex("#FF5252");
            }

            return background;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
