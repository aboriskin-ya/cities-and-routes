using DesktopApp.APIInteraction;
using DesktopApp.Models;
using DesktopApp.Services;
using DesktopApp.Services.Commands;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DesktopApp.ViewModels
{
    internal class MapViewModel : BaseViewModel, IMapViewModel
    {
        private readonly ICityAPIService _cityAPIService;
        private readonly IRouteAPIService _routeAPIService;
        private readonly IMessageBoxService _messageBoxService;

        public MapViewModel(ICityAPIService cityAPIService, IMessageBoxService messageBoxService, IRouteAPIService routeAPIService)
        {
            _cityAPIService = cityAPIService;
            _messageBoxService = messageBoxService;
            _routeAPIService = routeAPIService;

            WholeMap = new WholeMap()
            {
                Cities = new ObservableCollection<City>(),
                Routes = new ObservableCollection<Route>(),
                Settings = new Settings()
            };
            SelectedCity = new City();
            SelectedRoute = new Route();
        }

        private WholeMap wholeMap;
        public WholeMap WholeMap
        {
            get => wholeMap;
            set => Set(ref wholeMap, value, nameof(WholeMap));
        }

        private City selectedCity;
        public City SelectedCity
        {
            get => selectedCity;
            set => Set(ref selectedCity, value, nameof(SelectedCity));
        }

        private Route selectedRoute;
        public Route SelectedRoute
        {
            get => selectedRoute;
            set => Set(ref selectedRoute, value, nameof(SelectedRoute));
        }

        #region CityCommands

        public CreateCityCommand CreateNewCityCommand { get => new CreateCityCommand(p => OnCanAddCityCollection(), async m => await OnAddCityCollectionAsync()); }
        private async Task OnAddCityCollectionAsync()
        {
            try
            {
                SelectedCity.MapId = WholeMap.Id;
                var res = await _cityAPIService.CreateCityAsync(SelectedCity);
                if (!res.IsSuccessful)
                    throw new Exception();
                SelectedCity = res.Payload;
                WholeMap.Cities.Add(SelectedCity);
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex, "An error occured. Please try it again.");
            }
            finally
            {
                OnRemoveCityFromCollection();
            }
        }
        private bool OnCanAddCityCollection() => true;

        public CancelCreatingCityCommand CancelCreatingCityCommand { get => new CancelCreatingCityCommand(p => OnCanRemoveCityFromCollection(), m => OnRemoveCityFromCollection()); }
        private void OnRemoveCityFromCollection()
        {
            SelectedCity = new City();
        }
        private bool OnCanRemoveCityFromCollection() => true;

        #endregion

        #region RouteCommands

        public CreateRouteCommand CreateNewRouteCommand { get => new CreateRouteCommand(p => OnCanAddRouteCollection(), async m => await OnAddRouteCollectionAsync()); }
        private async Task OnAddRouteCollectionAsync()
        {
            try
            {
                SelectedRoute.MapId = WholeMap.Id;
                var res = await _routeAPIService.CreateRouteAsync(SelectedRoute);
                if (!res.IsSuccessful)
                    throw new Exception();
                SelectedRoute = res.Payload;
                WholeMap.Routes.Add(SelectedRoute);
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex, "An error occured. Please try it again.");
            }
            finally
            {
                OnRemoveRouteFromCollection();
            }
        }

        private bool OnCanAddRouteCollection() => true;

        public CancelCreatingRouteCommand CancelCreatingRouteCommand { get => new CancelCreatingRouteCommand(p => OnCanRemoveRouteFromCollection(), m => OnRemoveRouteFromCollection()); }
        private void OnRemoveRouteFromCollection()
        {
            SelectedRoute = new Route();
        }
        private bool OnCanRemoveRouteFromCollection() => true;

        #endregion

        public int CitiesCount() => WholeMap.Cities.Count;

        public int RoutesCount() => WholeMap.Routes.Count;

        public bool IsRouteHasBothCities() => SelectedRoute.FirstCity != null && SelectedRoute.SecondCity != null;

        public bool RouteWasSaved() => WholeMap.Routes.Contains(SelectedRoute);

        public bool CityWasSaved() => WholeMap.Cities.Contains(SelectedCity);

        public bool IsHaveMap() => WholeMap.Id != Guid.Empty;
    }
}