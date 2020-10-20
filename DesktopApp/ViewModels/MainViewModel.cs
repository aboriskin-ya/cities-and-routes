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
        internal MainViewModel()
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
                ScaleValue *= scale;
        }

        private bool OnCanZoomExecute(object p) => ( ScaleValue < 1.0  || ScaleValue >= 16.0) ? false : true;
        #endregion

        #region NavigateCommand
        public NavigateCommand NavigateCommand => new NavigateCommand(p => OnCanNavigateExecute(p), p => OnNavigateExecuted(p));

        private void OnNavigateExecuted(object p)
        {
            OffsetValue = (Point)p;
        }

        private bool OnCanNavigateExecute(object p)
        {
            MousePosition = (Point)p;
            if (MousePosition.X <= 1.0 && MousePosition.X >= 0 && MousePosition.Y <= 1.0 && MousePosition.Y >= 0) return true;
            else
            {
                switch (MousePosition)
                {
                    case Point i when i.X < 0: MousePosition = new Point(0, MousePosition.Y); break;
                    case Point i when i.Y < 0: MousePosition = new Point(MousePosition.X, 0); break;
                    case Point i when i.X > 1: MousePosition = new Point(1, MousePosition.Y); break;
                    case Point i when i.Y > 1: MousePosition = new Point(MousePosition.X, 1); break;
                    default:return true;
                }
            }
            return false;
        }
        #endregion

        #region OffsetValue
        private Point _OffsetValue;
        public Point OffsetValue
        {
            get => _OffsetValue;
            set => Set<Point>(ref _OffsetValue, value);
        }

        #endregion

        #region MousePosition
        private Point _MousePosition = new Point(0.5, 0.5);

        public Point MousePosition
        {
            get => _MousePosition;
            set => Set<Point>(ref _MousePosition, value);
        }
        #endregion
    }
}
