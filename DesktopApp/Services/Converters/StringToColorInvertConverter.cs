using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DesktopApp.Services.Converters
{

    public class StringToColorInvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color)ColorConverter.ConvertFromString(value as string);
            var invertColor = Color.FromArgb(color.A, (byte)~color.R, (byte)~color.G, (byte)~color.B);
            return new SolidColorBrush(invertColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}