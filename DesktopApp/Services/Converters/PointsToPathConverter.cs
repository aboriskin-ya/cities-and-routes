using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace DesktopApp.Services.Converters
{
    [ValueConversion(typeof(Point[]), typeof(Geometry))]
    public class PointsToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            if (value.GetType() == typeof(List<Point>) && targetType == typeof(PointCollection))
            {
                var pointCollection = new PointCollection();
                foreach (var point in value as List<Point>)
                    pointCollection.Add(point);
                return pointCollection;
            }
            if (value.GetType() == typeof(Point))
                return (Point)value;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}