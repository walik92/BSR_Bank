using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ClientApp.Helpers.Converters
{
    public class ColorOperationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((double) value >= 0)
                return (SolidColorBrush) new BrushConverter().ConvertFrom("#4F8A10");
            return (SolidColorBrush) new BrushConverter().ConvertFrom("#D8000C");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}