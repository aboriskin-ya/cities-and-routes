using Autofac;
using DesktopApp.Dialogs;
using DesktopApp.Models;
using DesktopApp.Resources;
using DesktopApp.Services.Commands;
using DesktopApp.Services.EventAggregator;
using DesktopApp.Services.Helper;
using GalaSoft.MvvmLight.Messaging;
using Prism.Events;
using System.IO;
using System.Threading.Tasks;
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
        private IMapViewModel mapViewModel;
        public IMapViewModel MapViewModel
        {
            get => mapViewModel;
            set => Set(ref mapViewModel, value, nameof(MapViewModel));
        }
        private ITravelSalesmanViewModel _travelViewModel;
        public ITravelSalesmanViewModel TravelSalesmanViewModel
        {
            get => _travelViewModel;
            set => Set(ref _travelViewModel, value);
        }
        public ICursorPositionViewModel PositionViewModel { get; }

        public IShortestPathViewModel ShortestPathViewModel { get; }
        private IEventAggregator _eventAggregator;

        public MainWindowViewModel(IMapViewModel viewModel,
                                   ICursorPositionViewModel positionViewModel,
                                   IShortestPathViewModel shortestPathViewModel,
                                   ITravelSalesmanViewModel travelViewModel,
                                   IEventAggregator eventAggregator)

        {
            TravelSalesmanViewModel = travelViewModel;
            MapViewModel = viewModel;
            PositionViewModel = positionViewModel;
            ShortestPathViewModel = shortestPathViewModel;

            InitializeModels();
            Messenger.Default.Register<WholeMap>(this, map => ReceiveMessageSelectExistingMap(map));
            TravelSalesmanViewModel.WasChanged += TravelSalesmanViewModel_WasChanged;

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<SettingsSentEvent>().Subscribe(ReceiveSettings, true);
            _eventAggregator.GetEvent<WholeMapSentEvent>().Subscribe(ReceiveMessageSelectExistingMap, true);
        }

        private void ReceiveSettings(Settings obj)
        {
            MapViewModel.WholeMap.Settings = obj;
        }

        public void ReceiveMessageSelectExistingMap(WholeMap map)
        {
            ShortestPathViewModel.InitializeModels();
            MapViewModel.InitializeModels();
            InitializeModels();
            InitializeMapViewModel(map);
            InitializeMapImageSource(map.Image.Data);
            TravelSalesmanViewModel.TravelsalesmanAcces = MapViewModel.IsHaveMap();
        }

        #region Initializers
        private void InitializeMapViewModel(WholeMap map)
        {
            MapViewModel.WholeMap = map;
        }

        private void InitializeMapImageSource(byte[] image)
        {
            var btmp = new BitmapImage();
            var memoryStream = new MemoryStream();

            memoryStream.Write(image);

            btmp.BeginInit();
            memoryStream.Seek(0, SeekOrigin.Begin);

            btmp.StreamSource = memoryStream;
            btmp.EndInit();

            MapImageSource = btmp;
            ImageHeight = (MapImageSource as BitmapImage).PixelHeight;
            ImageWidth = (MapImageSource as BitmapImage).PixelWidth;
        }

        private void InitializeModels()
        {
            AppState = new States();
            Path = new PathModel();
        }

        #endregion

        #region PathResolver
        public ICommand PathResolverOpenCommand => new PathResolverOpenCommand(p => OnCanPathResolverOpenExecute(p), p => OnPathResolverOpen(p));

        private void OnPathResolverOpen(object p)
        {
            AppState.IsAbleToFindShortestPath = true;
        }
        #region SelectCity
        public ICommand SelectCityCommand { get => TravelSalesmanViewModel.SelectCityCommand; }
        #endregion
        private bool canSelectedCitiesForPath;
        public bool CanSelectedCitiesForPath
        {
            get => canSelectedCitiesForPath;
            set => Set<bool>(ref canSelectedCitiesForPath, value);
        }

        private bool OnCanPathResolverOpenExecute(object p) => MapViewModel.IsHaveMap() && MapViewModel.RoutesCount() > 0;

        public ICommand AddingCitiesRoutesOpenCommand => new AddingCitiesRoutesOpenCommand(p => OnCanOnAddingCitiesRoutesOpenExecute(p), p => OnAddingCitiesRoutesOpen(p));

        private void OnAddingCitiesRoutesOpen(object p)
        {
            AppState.IsAbleToFindShortestPath = false;
            CanSelectedCitiesForPath = false;
        }

        private bool OnCanOnAddingCitiesRoutesOpenExecute(object p) => true;

        public ICommand CalculateShortestPathCommand => new RelayCommand(p => OnCalculateShortestPath(p), p => OnCanCalculateShortestPathExecute(p));

        private void OnCalculateShortestPath(object p)
        {
            path.MapId = MapViewModel.WholeMap.Id;
            ShortestPathViewModel.CalculateShortestPathCommand.Execute(path);
            Path = new PathModel();
        }

        private bool OnCanCalculateShortestPathExecute(object p) => Path.CityToId != default && Path.CityToId != Path.CityFromId;

        #endregion

        #region ShowCreateMapDialog 

        public ICommand ShowCreateMapDialogCommand => new ShowCreateMapDialogCommand(null, p => ShowCreateMapDialog(p));

        private void ShowCreateMapDialog(object p)
        {
            var model = RegisterServices.Configure().Resolve<CreateMapViewModel>(new NamedParameter("eventAggregator", _eventAggregator));
            var view = new CreateMapDialog { DataContext = model };
            view.Owner = App.Current.MainWindow;
            view.Show();
        }

        #endregion

        #region ShowSelectExistingMapDialog

        public ICommand ShowSelectExistingMapDialogCommand => new ShowCreateMapDialogCommand(null, p => ShowSelectExistingMapDialog(p));

        private void ShowSelectExistingMapDialog(object p)
        {
            var model = RegisterServices.Configure().Resolve<SelectExistingMapViewModel>(new NamedParameter("eventAggregator", _eventAggregator));
            var view = new SelectExistingMapDialog { DataContext = model };
            view.Owner = App.Current.MainWindow;
            view.Show();
        }

        #endregion

        #region ShowSettingsDialog

        public ICommand ShowSettingsDialogCommand => new RelayCommand(p => ShowSettingsDialog(p), p => OnCanShowSettingsDialogExecute(p));

        private void ShowSettingsDialog(object p)
        {
            var model = RegisterServices.Configure().Resolve<SettingsViewModel>(new NamedParameter("settings", MapViewModel.WholeMap.Settings),
                new NamedParameter("eventAggregator", _eventAggregator));
            var view = new SettingsDialog { DataContext = model };
            view.Owner = App.Current.MainWindow;
            view.Show();
        }

        private bool OnCanShowSettingsDialogExecute(object p) => MapViewModel.IsHaveMap();
        #endregion

        #region AddNewCityCommand
        public ICommand AddNewCityCommand => new AddNewCityCommand(p => OnCanAddNewCityExecute(p), p => OnAddNewCity(p));

        private void OnAddNewCity(object p)
        {
            AppState.IsAbleToSetCity = true;
            AppState.IsAbleToUpdateRoute = false;
        }

        private bool OnCanAddNewCityExecute(object p) => !AppState.IsAbleToSetCity && !AppState.IsAbleToCreateCity && !AppState.IsAbleToUpdateCity && MapViewModel.IsHaveMap();
        #endregion

        #region CreateNewCityCommand

        public ICommand CreateNewCityCommand => new RelayCommand(async p => await OnCreateNewCityExecutedAsync(p), p => OnCanCreateNewCityExecuted(p));

        private async Task OnCreateNewCityExecutedAsync(object HasError)
        {
            if ((bool)HasError == false)
            {
                await MapViewModel.CreateNewCityCommand.ExecuteAsync(HasError);
                AppState.IsAbleToCreateCity = false;
                if (MapViewModel.CityWasSaved())
                    AppState.IsSuccess = true;
            }
        }

        private bool OnCanCreateNewCityExecuted(object p) => AppState.IsAbleToCreateCity;

        #endregion

        #region UpdateCityCommand

        public ICommand UpdateCityCommand => new RelayCommand(p => OnUpdateCityExecuted(p), p => OnCanUpdateCityExecuted(p));

        private void OnUpdateCityExecuted(object HasError)
        {
            if ((bool)HasError == false)
            {
                MapViewModel.UpdateCityCommand.Execute(HasError);
                AppState.IsAbleToCreateCity = false;
                AppState.IsAbleToSetCity = false;
                AppState.IsAbleToUpdateCity = false;
                if (MapViewModel.CityWasSaved())
                    AppState.IsSuccess = true;
            }
        }

        private bool OnCanUpdateCityExecuted(object p) => AppState.IsAbleToUpdateCity;

        #endregion

        #region AddNewRouteCommand

        public ICommand AddNewRouteCommand => new AddNewRouteCommand(p => OnCanAddNewRouteExecute(p), p => OnAddNewRoute(p));

        private void OnAddNewRoute(object p)
        {
            AppState.IsAbleToPickFirstCity = true;
            AppState.IsAbleToUpdateCity = false;
            AppState.IsAbleToUpdateRoute = false;
        }

        private bool OnCanAddNewRouteExecute(object p) => !AppState.IsAbleToCreateRoute
            && !AppState.IsAbleToPickFirstCity
            && MapViewModel.CitiesCount() >= 2;

        #endregion

        #region CreateNewRouteCommand

        public ICommand CreateNewRouteCommand => new RelayCommand(async p => await OnCreateNewRouteExecutedAsync(p), p => OnCanCreateNewRouteExecuted(p));

        private async Task OnCreateNewRouteExecutedAsync(object HasError)
        {
            if ((bool)HasError == false)
            {
                await MapViewModel.CreateNewRouteCommand.ExecuteAsync(HasError);
                AppState.IsAbleToCreateRoute = false;
                if (MapViewModel.RouteWasSaved())
                    AppState.IsSuccess = true;
            }
        }

        private bool OnCanCreateNewRouteExecuted(object p) => MapViewModel.IsRouteHasBothCities();

        #endregion

        #region UpdateRouteCommand

        public ICommand UpdateRouteCommand => new RelayCommand(p => OnUpdateRouteExecuted(p), p => OnCanUpdateRouteExecuted(p));

        private void OnUpdateRouteExecuted(object HasError)
        {
            if ((bool)HasError == false)
            {
                MapViewModel.UpdateRouteCommand.Execute(HasError);
                AppState.IsAbleToCreateCity = false;
                AppState.IsAbleToSetCity = false;
                AppState.IsAbleToUpdateRoute = false;
                if (MapViewModel.RouteWasSaved())
                    AppState.IsSuccess = true;
            }
        }

        private bool OnCanUpdateRouteExecuted(object p) => AppState.IsAbleToUpdateRoute;

        #endregion

        #region DeleteRouteCommand

        public ICommand DeleteRouteCommand => new DeleteRouteCommand(p => OnCanDeleteRouteExecuted(p), p => OnDeleteRouteExecuted(p));

        private void OnDeleteRouteExecuted(object p)
        {
            var Message = "Are you sure, you want to delete the route?";
            MessageBoxResult DialogResult = MessageBox.Show(Message, "Confirm action", MessageBoxButton.YesNo);
            if (DialogResult == MessageBoxResult.Yes && AppState.IsAbleToUpdateRoute)
            {
                MapViewModel.DeleteRouteCommand.Execute(p);
                ShortestPathViewModel.InitializeModels();
                AppState.IsAbleToUpdateRoute = false;
                AppState.IsSuccess = true;
            }
        }

        private bool OnCanDeleteRouteExecuted(object p) => AppState.IsAbleToUpdateRoute;

        #endregion

        #region CancelCreatingNewCityCommand
        public ICommand CancelCreatingCityCommand => new CancelCreatingCityCommand(p => OnCanCancelCreatingCityExecuted(p), p => OnCancelCreatingCityExecuted(p));

        private void OnCancelCreatingCityExecuted(object p)
        {
            MapViewModel.CancelCreatingCityCommand.Execute(p);
            AppState.IsAbleToCreateCity = false;
        }

        private bool OnCanCancelCreatingCityExecuted(object p) => AppState.IsAbleToCreateCity;
        #endregion

        #region DeleteCityCommand
        public ICommand DeleteCityCommand => new DeleteCityCommand(p => OnCanDeleteCityExecuted(p), p => DeleteCityCommandExecuted(p));

        private void DeleteCityCommandExecuted(object p)
        {
            var Message = "Are you sure, you want to delete {0} city?";
            MessageBoxResult DialogResult = MessageBox.Show(string.Format(Message, MapViewModel.SelectedCity.Name), "Confirm action", MessageBoxButton.YesNo);
            if (DialogResult == MessageBoxResult.Yes && AppState.IsAbleToUpdateCity)
            {
                MapViewModel.DeleteCityCommand.Execute(p);
                ShortestPathViewModel.InitializeModels();
                TravelSalesmanViewModel.Initialize();                
                AppState.IsAbleToUpdateCity = false;
                AppState.IsSuccess = true;
            }
        }

        private bool OnCanDeleteCityExecuted(object p) => AppState.IsAbleToUpdateCity;
        #endregion

        #region CancelCreatingNewCityCommand
        public ICommand CancelCreatingRouteCommand => new CancelCreatingRouteCommand(p => OnCanCancelCreatingRouteExecuted(p), p => OnCancelCreatingRouteExecuted(p));

        private void OnCancelCreatingRouteExecuted(object p)
        {
            MapViewModel.CancelCreatingRouteCommand.Execute(p);
            AppState.IsAbleToCreateRoute = false;
        }

        private bool OnCanCancelCreatingRouteExecuted(object p) => AppState.IsAbleToCreateRoute;
        #endregion

        #region ResolveTravelSalesmanCommand
        public ICommand ResolveTravelSalesmanCommand { get => TravelSalesmanViewModel.ResolveTravelSalesmanCommand; }
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
        private double _ScaleValue = 1.0;
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
        public ZoomCommand ZoomCommand => new ZoomCommand(p => MapViewModel.IsHaveMap(), p => OnZoomExecuted(p));

        private void OnZoomExecuted(object p)
        {
            if (double.TryParse(p.ToString(), out double scale))
            {
                switch (scale)
                {
                    case 2:
                        if (ScaleValue >= 1 && ScaleValue < 16)
                        {
                            ScaleValue *= scale;
                            Offset = MapHelper.GetOffset(Offset, ScaleValue, ImageHeight, ImageWidth, TransformPosition, ZoomEnum.ZoomIn);
                            ImageHeight /= 2; ImageWidth /= 2;
                        }
                        break;

                    case 0.5:
                        if (ScaleValue > 1 && ScaleValue <= 16)
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

        #region ApplicationState

        private States appState;
        public States AppState
        {
            get => appState;
            set => Set<States>(ref appState, value, nameof(AppState));
        }

        #endregion

        private PathModel path;
        public PathModel Path
        {
            get => path;
            set => Set(ref path, value, nameof(Path));
        }
        private void TravelSalesmanViewModel_WasChanged(object sender, System.EventArgs e)
        {
            var travelsalesman = sender as TravelSalesmanViewModel;
            CanSelectedCitiesForPath = travelsalesman.CanSelectCities;
        }
    }
}