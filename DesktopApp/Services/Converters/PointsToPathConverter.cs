using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace DesktopApp.Services.Converters
{
    [ValueConversion(typeof(Point[]), typeof(Geometry))]
    public class PointsToPathConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value = values[0];
            var ActualWidth = (double)values[1];
            var InitialWidth = (double)values[2];
            var ActualHeight = (double)values[3];
            var InitialHeight = (double)values[4];

            if (value == null) return null;
            if (value.GetType() == typeof(List<Point>) && targetType == typeof(PointCollection))
            {
                var pointCollection = new PointCollection();
                foreach (var point in value as List<Point>)
                {
                    var newPoint = point;
                    newPoint.X = point.X * ((ActualWidth / (InitialWidth / 100)) / 100);
                    newPoint.Y = point.Y * ((ActualHeight / (InitialHeight / 100)) / 100);
                    pointCollection.Add(newPoint);
                }
                return pointCollection;
            }
            if (value.GetType() == typeof(Point))
            {
                var newPoint = (Point)value;
                newPoint.X = newPoint.X * ((ActualWidth / (InitialWidth / 100)) / 100);
                newPoint.Y = newPoint.Y * ((ActualHeight / (InitialHeight / 100)) / 100);
                return newPoint;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}