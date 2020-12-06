using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DesktopApp.Services.Converters
{
    class SizeCorrectorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }

            var size = (double)values[0];
            var ScaleValue = (double)values[1];

            if (ScaleValue == 0)
                return size;

            return size / ScaleValue;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}