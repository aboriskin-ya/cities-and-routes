﻿using DesktopApp.APIInteraction;
using DesktopApp.Models;
using DesktopApp.Services;
using DesktopApp.Services.Commands;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    internal class MapViewModel : BaseViewModel, IMapViewModel
    {
        private readonly ICityAPIService _cityAPIService;
        private readonly IRouteAPIService _routeAPIService;
        private readonly IMessageBoxService _messageBoxService;

        public MapViewModel(ICityAPIService cityAPIService,
               IMessageBoxService messageBoxService,
               IRouteAPIService routeAPIService)
        {
            _cityAPIService = cityAPIService;
            _messageBoxService = messageBoxService;
            _routeAPIService = routeAPIService;

            SelectedCity = new City();

            WholeMap = new WholeMap()
            {
                Name = "a map is not chosen",
                Cities = new ObservableCollection<City>(),
                Routes = new ObservableCollection<Route>(),
                Settings = new Settings()
            };
            InitializeModels();
        }
        public ObservableCollection<City> SelectedCities { get; set; }

        private WholeMap wholeMap;
        public WholeMap WholeMap
        {
            get => wholeMap;
            set => Set(ref wholeMap, value, nameof(WholeMap));
        }

        private Route _selectedRoute;
        public Route SelectedRoute
        {
            get => _selectedRoute;
            set => Set<Route>(ref _selectedRoute, value);
        }
        private City selectedCity;
        public City SelectedCity
        {
            get => selectedCity;
            set => Set(ref selectedCity, value, nameof(SelectedCity));
        }


        #region CityCommands
        public RelayCommandAsync CreateNewCityCommand { get => new RelayCommandAsync(p => OnCanAddCityCollection(), async m => await OnAddCityCollectionAsync()); }
        private async Task OnAddCityCollectionAsync()
        {
            SelectedCity.MapId = WholeMap.Id;
            var res = await _cityAPIService.CreateCityAsync(SelectedCity);
            if (!res.IsSuccessful)
                _messageBoxService.ShowError("An error occured. Please try it again.", "Failed result");
            else
            {
                SelectedCity = res.Payload;
                WholeMap.Cities.Add(SelectedCity);
            }
        }
        private bool OnCanAddCityCollection() => true;

        public ICommand UpdateCityCommand { get => new RelayCommand(async m => await OnUpdateCityCollectionAsync(), p => OnCanUpdateCityCollection()); }
        private async Task OnUpdateCityCollectionAsync()
        {
            var res = await _cityAPIService.UpdateCityAsync(SelectedCity);
            if (!res.IsSuccessful)
            {
                _messageBoxService.ShowError("An error occured. Please try it again.", "Error occured");
            }
        }

        private bool OnCanUpdateCityCollection() => true;

        public ICommand CancelCreatingCityCommand { get => new RelayCommand(m => OnRemoveCityFromCollection(), p => OnCanRemoveCityFromCollection()); }
        private void OnRemoveCityFromCollection()
        {
            SelectedCity = new City();
        }
        private bool OnCanRemoveCityFromCollection() => true;

        public ICommand DeleteCityCommand { get => new RelayCommand(async m => await OnDeleteCityFromCollection(), p => OnCanDeleteCityFromCollection()); }
        private async Task OnDeleteCityFromCollection()
        {
            var res = await _cityAPIService.DeleteCityAsync(SelectedCity);
            if (res.IsSuccessful)
            {
                for (int i = WholeMap.Routes.Count - 1; i >= 0; i--)
                {
                    var Route = WholeMap.Routes[i];
                    if (Route.FirstCity.Id == SelectedCity.Id || Route.SecondCity.Id == SelectedCity.Id)
                    {
                        WholeMap.Routes.Remove(Route);
                    }
                }
                WholeMap.Cities.Remove(SelectedCity);
                OnRemoveCityFromCollection();
            }
            else
            {
                _messageBoxService.ShowError("An error occured. Please try it again.", "Error occured");
            }
        }
        private bool OnCanDeleteCityFromCollection() => true;

        #endregion

        #region RouteCommands

        public RelayCommandAsync CreateNewRouteCommand { get => new RelayCommandAsync(p => OnCanAddRouteCollection(), async m => await OnAddRouteCollectionAsync()); }
        private async Task OnAddRouteCollectionAsync()
        {
            SelectedRoute.MapId = WholeMap.Id;
            var res = await _routeAPIService.CreateRouteAsync(SelectedRoute);
            if (!res.IsSuccessful)
                _messageBoxService.ShowError("An error occured. Please try it again.", "Failed result");
            else
            {
                SelectedRoute = res.Payload;
                WholeMap.Routes.Add(SelectedRoute);
            }
        }

        private bool OnCanAddRouteCollection() => true;

        public ICommand CancelCreatingRouteCommand { get => new RelayCommand(m => OnRemoveRouteFromCollection(m), p => OnCanRemoveRouteFromCollection()); }
        private void OnRemoveRouteFromCollection(object Button)
        {
            (Button as ToggleButton).IsChecked = false;
            SelectedRoute = new Route();
        }
        private bool OnCanRemoveRouteFromCollection() => true;

        public ICommand UpdateRouteCommand { get => new RelayCommand(async m => await OnUpdateRouteCollectionAsync(), p => OnCanUpdateRouteCollection()); }
        private async Task OnUpdateRouteCollectionAsync()
        {
            var res = await _routeAPIService.UpdateRouteAsync(SelectedRoute);
            if (!res.IsSuccessful)
            {
                _messageBoxService.ShowError("An error occured. Please try it again.", "Error occured");
            }
        }

        private bool OnCanUpdateRouteCollection() => true;

        public ICommand DeleteRouteCommand { get => new RelayCommand(async m => await OnDeleteRouteFromCollection(), p => OnCanDeleteRouteFromCollection()); }
        private async Task OnDeleteRouteFromCollection()
        {
            var res = await _routeAPIService.DeleteRouteAsync(SelectedRoute);
            if (res.IsSuccessful)
            {
                WholeMap.Routes.Remove(SelectedRoute);
                SelectedRoute = new Route();
            }
            else
            {
                _messageBoxService.ShowError("An error occured. Please try it again.", "Error occured");
                SelectedRoute = new Route();
            }
        }
        private bool OnCanDeleteRouteFromCollection() => true;

        #endregion

        public int CitiesCount() => WholeMap.Cities.Count;

        public int RoutesCount() => WholeMap.Routes.Count;

        public bool IsRouteHasBothCities() => SelectedRoute.FirstCity != null && SelectedRoute.SecondCity != null;

        public bool RouteWasSaved() => WholeMap.Routes.Contains(SelectedRoute);

        public bool CityWasSaved() => WholeMap.Cities.Contains(SelectedCity);

        public bool IsHaveMap() => WholeMap.Id != default;

        public void InitializeModels()
        {
            SelectedCity = new City();
            SelectedRoute = new Route();
        }
    }
}