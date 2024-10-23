using System;
using System.Globalization;
using System.Windows.Data;

namespace ZeroTier.Utils
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool authorized)
            {
                return authorized ? "Authorized" : "Denied";
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
