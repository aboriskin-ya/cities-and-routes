using DevExpress.Mvvm.Native;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DesktopApp.Services.Converters
{

    class DateTimeUTCToLocalTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTimeOffset)
            {
                var currentTimeZone = TimeZoneInfo.Local;
                value = TimeZoneInfo.ConvertTime((DateTimeOffset)value, currentTimeZone);
                return value;
            }
            else return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
