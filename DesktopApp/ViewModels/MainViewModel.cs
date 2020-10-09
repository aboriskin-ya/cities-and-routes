using DesktopApp.Services.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

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

        #region ZoomInCommand
        public BaseCommand ZoomInCommand => new BaseCommand((p) => OnCanZoomInExecute(p), (p) => OnZoomInExecuted(p));

        private void OnZoomInExecuted(object p)
        {
           var image= (p is Image) ? p as Image : null;
            if (image != null)
            {
                ScaleValue *= 2;
                ScaleTransform transform = new ScaleTransform(ScaleValue,ScaleValue);
                image.RenderTransform = transform;
            }
        }

        private bool OnCanZoomInExecute(object p) => true;
        #endregion

        #region ZoomOut
        public BaseCommand ZoomOutCommand => new BaseCommand((p) => OnCanZoomOutExecute(p), (p) => OnZoomOutExecuted(p));

        private void OnZoomOutExecuted(object p)
        {
            var image = (p is Image) ? p as Image : null;
            if (image != null)
            {
                ScaleValue /= 2;
                ScaleTransform transform = new ScaleTransform(ScaleValue, ScaleValue);
                image.RenderTransform = transform;
            }
        }

        private bool OnCanZoomOutExecute(object p) => true;
       
        #endregion
    }
}
