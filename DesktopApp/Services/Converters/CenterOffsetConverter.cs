using System;
using System.Globalization;
using System.Windows.Data;

namespace DesktopApp.Services.Converters
{
    public class CenterOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -(double)value / 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
