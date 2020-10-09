using System;
using System.Collections.Generic;
using System.Text;
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

    }
}
