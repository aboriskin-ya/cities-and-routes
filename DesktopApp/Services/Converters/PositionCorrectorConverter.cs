﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DesktopApp.Services.Converters
{
    public class PositionCorrectorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue)
            {
                return DependencyProperty.UnsetValue;
            }
            var XY = (double)values[0];
            var Actual = (double)values[1];
            var Initial = (double)values[2];
            return XY * (Actual / Initial);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
