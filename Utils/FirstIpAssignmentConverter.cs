using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace ZeroTier.Utils
{
    public class FirstIpAssignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Vérifie si la valeur est une liste de chaînes
            if (value is List<string> ipAssignments && ipAssignments.Count > 0)
            {
                return ipAssignments[0]; // Renvoie la première adresse IP
            }
            return ""; // Valeur par défaut
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}