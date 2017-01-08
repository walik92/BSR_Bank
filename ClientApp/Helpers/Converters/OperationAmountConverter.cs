using System;
using System.Globalization;
using System.Windows.Data;

namespace ClientApp.Helpers.Converters
{
    public class OperationAmountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (double) value;
            if ((double) value >= 0)
                return $"+{val.ToString("N2", CultureInfo.CreateSpecificCulture("en-US"))}";
            return $"{val.ToString("N2", CultureInfo.CreateSpecificCulture("en-US"))}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}