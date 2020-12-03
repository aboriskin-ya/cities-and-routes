﻿using System.Windows;
using System.Windows.Data;

namespace DesktopApp.Services.Converters
{
    class MultiPaddingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            for (int i = 0; i < values.Length; i++)
                if (values[i] == DependencyProperty.UnsetValue)
                    values[i] = 0;
            return new Thickness(System.Convert.ToDouble(values[0]),
                                 System.Convert.ToDouble(values[1]),
                                 System.Convert.ToDouble(values[2]),
                                 System.Convert.ToDouble(values[3]));
        }

        public object[] ConvertBack(object value, System.Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}