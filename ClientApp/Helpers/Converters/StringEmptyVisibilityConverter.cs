using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ClientApp.Helpers.Converters
{
    public class StringEmptyVisibilityConverter
    {
        public class NullVisibilityConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return string.IsNullOrEmpty(value as string) ? Visibility.Collapsed : Visibility.Visible;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}