using DesktopApp.Services.Commands;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DesktopApp.ViewModels
{
    internal class MainViewModel:BaseViewModel
    {
        public MainViewModel()
        {
            MapImageSource = new BitmapImage(new Uri("/Resources/Maps/mapOfRussia.jpg", UriKind.Relative));
        }
        #region MapImage
        private ImageSource _mapImage;
        public ImageSource MapImageSource
        {
            get => _mapImage;
            set => Set<ImageSource>(ref _mapImage, value, nameof(MapImageSource));
        }
        #endregion

        #region ScaleValue
        private double _ScaleValue=1.0;
        public double ScaleValue
        {
            get => _ScaleValue;
            set => Set<double>(ref _ScaleValue, value, nameof(ScaleValue));
        }
        #endregion

        #region ZoomCommand
        public ZoomCommand ZoomCommand => new ZoomCommand(p => OnCanZoomExecute(p), p => OnZoomExecuted(p));

        private void OnZoomExecuted(object p)
        {
            if (double.TryParse(p.ToString(), out double scale))
            {
                switch (scale)
                {
                    case 2:if (ScaleValue >= 1 && ScaleValue < 16) ScaleValue *= scale;break;
                    case 0.5: if (ScaleValue > 1 && ScaleValue <= 16) ScaleValue *= scale; break;
                }
            }
               
        }

        private bool OnCanZoomExecute(object p) => true;
        #endregion

        #region NavigateCommand
        public NavigateCommand NavigateCommand => new NavigateCommand(p => OnCanNavigateExecute(p), p => OnNavigateExecuted(p));

        private void OnNavigateExecuted(object p)
        {
            OffsetValue = (Point)p;
        }

        private bool OnCanNavigateExecute(object p) => true;
        
        #endregion

        #region OffsetValue
        private Point _OffsetValue;
        public Point OffsetValue
        {
            get => _OffsetValue;
            set => Set<Point>(ref _OffsetValue, value);
        }

        #endregion

        #region TransformPosition
        private Point _TransformPosition = new Point(0.5, 0.5);

        public Point TransformPosition
        {
            get => _TransformPosition;
            set => Set<Point>(ref _TransformPosition, value);
        }
        #endregion
    }
}
