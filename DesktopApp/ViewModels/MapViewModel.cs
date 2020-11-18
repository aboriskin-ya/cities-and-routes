using DesktopApp.APIInteraction;
using DesktopApp.Models;
using DesktopApp.Service;
using DesktopApp.Services.Commands;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
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

            CityCollection = new ObservableCollection<City>();
            SelectedCities = new ObservableCollection<City>();
            SelectedCity = new City();
            RouteCollection = new ObservableCollection<Route>();
            SelectedRoute = new Route();
            SettingsMap = new Settings()
            {
                VertexColor = "#ff0000",
                VertexSize = 10,
                EdgeColor = "#dde3ed",
                EdgeSize = 3
            };
        }

        public ObservableCollection<City> CityCollection { get; set; }
        public ObservableCollection<Route> RouteCollection { get; set; }
        public ObservableCollection<City> SelectedCities { get; set; }
        private City _SelectedCity;
        public City SelectedCity
        {
            get => _SelectedCity;
            set => Set<City>(ref _SelectedCity, value, nameof(SelectedCity));
        }
        private City _highlightedCity;
        public City HighLightedCity
        {
            get => _highlightedCity;
            set => Set<City>(ref _highlightedCity, value);
        }
        private bool _canSelected = false;
        public bool CanSelected
        {
            get => _canSelected;
            set => Set<bool>(ref _canSelected, value);
        }
        private Route _SelectedRoute;
        public Route SelectedRoute
        {
            get => _SelectedRoute;
            set => Set<Route>(ref _SelectedRoute, value, nameof(SelectedRoute));
        }

        private Settings _SettingsMap;
        public Settings SettingsMap
        {
            get => _SettingsMap;
            set => Set<Settings>(ref _SettingsMap, value, nameof(SettingsMap));
        }

        #region CityCommands
        public SelectCityCommand SelectCityCommand { get => new SelectCityCommand(p => OnCanSelectCityExecute(p), p => OnSelectCityExecuted(p)); }

        private void OnSelectCityExecuted(object p)
        {
            if (p is City)
                HighLightedCity = (City)p;
            SelectedCities.Add(HighLightedCity);
        }

        private bool OnCanSelectCityExecute(object p) => CanSelected;
           
        

        public CreateCityCommand CreateNewCityCommand { get => new CreateCityCommand(p => OnCanAddCityCollection(), async m => await OnAddCityCollectionAsync()); }
        private async Task OnAddCityCollectionAsync()
        {
            try
            {
                var res = await _cityAPIService.CreateCityAsync(SelectedCity);
                if (!res.IsSuccessful)
                    throw new Exception();
                SelectedCity = res.Payload;
                CityCollection.Add(SelectedCity);
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex, "An error occured. Please try it again.");
                OnRemoveCityFromCollection();
            }
        }
        private bool OnCanAddCityCollection() => true;

        public CancelCreatingCityCommand CancelCreatingCityCommand { get => new CancelCreatingCityCommand(p => OnCanRemoveCityFromCollection(), m => OnRemoveCityFromCollection()); }
        private void OnRemoveCityFromCollection()
        {
            CityCollection.Remove(SelectedCity);
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
                var res = await _routeAPIService.CreateRouteAsync(SelectedRoute);
                if (!res.IsSuccessful)
                    throw new Exception();
                SelectedRoute = res.Payload;
                RouteCollection.Add(SelectedRoute);
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex, "An error occured. Please try it again.");
                OnRemoveCityFromCollection();
            }
            SelectedRoute = new Route();
        }

        private bool OnCanAddRouteCollection() => true;

        public CancelCreatingRouteCommand CancelCreatingRouteCommand { get => new CancelCreatingRouteCommand(p => OnCanRemoveRouteFromCollection(), m => OnRemoveRouteFromCollection()); }
        private void OnRemoveRouteFromCollection()
        {
            RouteCollection.Remove(SelectedRoute);
            SelectedRoute = new Route();
        }
        private bool OnCanRemoveRouteFromCollection() => true;

        #endregion

        public int CitiesCount() => CityCollection.Count;

        public int RoutesCount() => RouteCollection.Count;

        public bool IsRouteHasBothCities() => SelectedRoute.FirstCity != null && SelectedRoute.SecondCity != null;

        public bool RouteWasSaved() => RouteCollection.Contains(SelectedRoute);

        public bool CityWasSaved() => CityCollection.Contains(SelectedCity);
    }
}
