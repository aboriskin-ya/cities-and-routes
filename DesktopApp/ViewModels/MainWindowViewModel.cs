using Autofac;
using DesktopApp.Dialogs;
using DesktopApp.Services.Commands;
using DesktopApp.Services.Helper;
using DesktopApp.Services.State;
using DesktopApp.UserControllers;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DesktopApp.ViewModels
{
    public enum ZoomEnum
    {
        ZoomIn, ZoomOut
    }
    internal class MainWindowViewModel : BaseViewModel
    {
        public IMapViewModel MapViewModel { get; }

        public ICursorPositionViewModel PositionViewModel { get; }

        public MainWindowViewModel(IMapViewModel viewModel, ICursorPositionViewModel positionViewModel)
        {
            MapImageSource = new BitmapImage(new Uri(@"Resources\Maps\USAMap.jpg", UriKind.Relative));
            ImageHeight = (MapImageSource as BitmapImage).PixelHeight;
            ImageWidth = (MapImageSource as BitmapImage).PixelWidth;

            MapViewModel = viewModel;
            PositionViewModel = positionViewModel;
        }

        #region ShowCreateMapDialog     
        public ICommand ShowCreateMapDialogCommand => new ShowCreateMapDialogCommand(null, p => ShowDialog(p));

        private void ShowDialog(object p)
        {
            var model = RegisterServices.Configure().Resolve<CreateMapViewModel>();
            var view = new CreateMapDialog { DataContext = model };
            view.Owner = App.Current.MainWindow;
            view.Show();
        }
        #endregion

        #region AddNewCityCommand
        public ICommand AddNewCityCommand => new AddNewCityCommand(p => OnCanAddNewCityExecute(p), p => OnAddNewCity(p));

        private void OnAddNewCity(object p)
        {
            StatusBar = StateLine.Show(StateLineStatus.SetMap);
            IsAbleToSetCity = true;
        }

        private bool OnCanAddNewCityExecute(object p) => !IsAbleToSetCity && !IsAbleToCreateCity;//&& Map != null;
        #endregion

        #region CreateNewCityCommand
        public ICommand CreateNewCityCommand => new CreateCityCommand(p => OnCanCreateNewCityExecuted(p), p => OnCreateNewCityExecuted(p));

        private void OnCreateNewCityExecuted(object p)
        { 
            MapViewModel.CreateNewCityCommand.Execute(p);
            IsAbleToCreateCity = false;
        }

        private bool OnCanCreateNewCityExecuted(object p) => IsAbleToCreateCity && MapViewModel.SelectedCity.Name != null;
        #endregion

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

        #region ImageHeight
        private double _ImageHeight = 1.0;
        public double ImageHeight
        {
            get => _ImageHeight;
            set => Set<double>(ref _ImageHeight, value, nameof(ImageHeight));
        }
        #endregion

        #region ImageWidth
        private double _ImageWidth = 1.0;
        public double ImageWidth
        {
            get => _ImageWidth;
            set => Set<double>(ref _ImageWidth, value, nameof(ImageWidth));
        }
        #endregion

        #region ZoomCommand
        public ZoomCommand ZoomCommand => new ZoomCommand(p =>true, p => OnZoomExecuted(p));

        private void OnZoomExecuted(object p)
        {
            if (double.TryParse(p.ToString(), out double scale))
            {
                switch (scale)
                {
                    case 2:if (ScaleValue >= 1 && ScaleValue < 16)
                        {
                            ScaleValue *= scale;
                            Offset = MapHelper.GetOffset(Offset, ScaleValue,ImageHeight,ImageWidth,TransformPosition, ZoomEnum.ZoomIn);
                            ImageHeight /= 2; ImageWidth /= 2;
                        }
                        break;
                      
                    case 0.5: if (ScaleValue > 1 && ScaleValue <= 16)
                        {
                            ScaleValue *= scale;
                            ImageHeight *= 2; ImageWidth *= 2;
                            Offset = MapHelper.GetOffset(Offset, ScaleValue, ImageHeight, ImageWidth, TransformPosition, ZoomEnum.ZoomOut);
                            
                        }
                        break;
                        
                }
            }
               
        }

        private bool OnCanZoomExecute(object p) => true;
        #endregion

        #region OffsetProperty
        private Offset _Offset;
        public Offset Offset
        {
            get => _Offset;
            set => Set<Offset>(ref _Offset, value);
        }
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

        #region PixelPerWidth
        private double _PPW;
        public double PPW
        {
            get => _PPW;
            set => Set<double>(ref _PPW, value);
        }
        #endregion

        #region PixelPerHeight
        private double _PPH;
        public double PPH
        {
            get => _PPH;
            set => Set<double>(ref _PPH, value);
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

        #region StateLine

        private string statusBar = StateLine.Show(StateLineStatus.AddMap);
        public string StatusBar
        {
            get => statusBar;
            set => Set<string>(ref statusBar, value);
        }

        #endregion

        #region CreateCityPossibility

        private bool _IsAbleToCreateCity = false;
        public bool IsAbleToCreateCity
        {
            get => _IsAbleToCreateCity;
            set => Set<bool>(ref _IsAbleToCreateCity, value);
        }

        #endregion

        #region SetCityPossibility

        private bool _IsAbleToSetCity = false;
        public bool IsAbleToSetCity
        {
            get => _IsAbleToSetCity;
            set => Set<bool>(ref _IsAbleToSetCity, value);
        }

        #endregion
    }
}
