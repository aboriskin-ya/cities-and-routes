using DesktopApp.APIInteraction;
using DesktopApp.Models;
using DesktopApp.Service;
using DesktopApp.Services.Commands;
using DesktopApp.Services.State;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.ViewModels
{
    internal class MapViewModel : BaseViewModel, IMapViewModel
    {
        private readonly ICityAPIService _cityAPIService;
        private readonly IRouteAPIService _routeAPIService;
        private readonly ITravelSalesmanService _travelSalesmanService;
        private readonly IMessageBoxService _messageBoxService;
        public enum SelectedMethod
        {
            Annealing,
            Nearest,
            Quickest
        }
        public MapViewModel(ICityAPIService cityAPIService,
               IMessageBoxService messageBoxService,
               IRouteAPIService routeAPIService,
               ITravelSalesmanService travelSalesmanService)
        {
            _cityAPIService = cityAPIService;
            _messageBoxService = messageBoxService;
            _routeAPIService = routeAPIService;
            _travelSalesmanService = travelSalesmanService;
            

            CityCollection = new ObservableCollection<City>();
            SelectedCities = new ObservableCollection<City>();
            SelectedCity = new City();
            RouteCollection = new ObservableCollection<Route>();
            SelectedRoute = new Route();
            State = StateLine.GetStatus(StateBar.PushButton);
            SettingsMap = new Settings()
            {
                VertexColor = "#ff0000",
                VertexSize = 10,
                EdgeColor = "#dde3ed",
                EdgeSize = 3
            };
        }

        #region SelectedMethodIndex
       
        private int _selectedMethodIndex;
        public int SelectedMethodIndex
        {
            get => _selectedMethodIndex;
            set => Set<int>(ref _selectedMethodIndex, value);
        }
        #endregion
        public ObservableCollection<City> CityCollection { get; set; }
        public ObservableCollection<Route> RouteCollection { get; set; }
        public ObservableCollection<City> SelectedCities { get; set; }
        private City _SelectedCity;

        public City SelectedCity
        {
            get => _SelectedCity;
            set => Set<City>(ref _SelectedCity, value, nameof(SelectedCity));
        }

        #region HighlitedCity
        private City _highlightedCity;
        public City HighLightedCity
        {
            get => _highlightedCity;
            set => Set<City>(ref _highlightedCity, value);
        }
        #endregion

        #region CanSelectCities
        private bool _canSelected = false;
        public bool CanSelected
        {
            get => _canSelected;
            set
            {
                if(value==false)
                    State = StateLine.GetStatus(StateBar.PushButton);
                else State = StateLine.GetStatus(StateBar.SelectCities);
                Set<bool>(ref _canSelected, value);
                
            }
        }
        #endregion

        #region ConsoleText
        private string _consoleText;
        public string ConsoleText
        {
            get => _consoleText;
            set => Set<string>(ref _consoleText, value);
        }
        #endregion

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
        private string _state;
        public string State
        {
            get => _state;
            set => Set<string>(ref _state, value);
        }
        #region CityCommands
        public SelectCityCommand SelectCityCommand { get => new SelectCityCommand(p => OnCanSelectCityExecute(p), p => OnSelectCityExecuted(p)); }

        private void OnSelectCityExecuted(object p)
        {
            if (p is City)
                HighLightedCity = (City)p;
            SelectedCities.Add(HighLightedCity);
            ConsoleText += $"{HighLightedCity.Name} ";
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

        #region TravelSalesmanCommand
        public ResolveTravelSalesmanCommand ResolveTravelSalesmanCommand { get => new ResolveTravelSalesmanCommand(p => OnCanResolveExecute(p),
                                                                            async p => await OnResolveExecuted());
        }
        
        private async Task OnResolveExecuted()
        {
            State = StateLine.GetStatus(StateBar.ResolvingGoal);
            IEnumerable<Guid> idCollection = SelectedCities.Select(t => t.Id);
            var model = await _travelSalesmanService.PostCities(idCollection, SelectedMethodIndex);
            var builder = new StringBuilder();
            builder.Append($"\nAlgorithm: {model.Payload.NameAlghorithm}\n" +
                           $"Process` duration: {model.Payload.ProcessDuration}\n" +
                           $"Calculated distance: {model.Payload.CalculatedDistance}\n" +
                           $"Preferable sequence: ");
            foreach (var cityId in model.Payload.PreferableSequenceOfCities)
            {
                var city = await _cityAPIService.GetCity(cityId);
                builder.Append($"{city.Payload.Name} ");
            }
            ConsoleText += builder.ToString();
        }

        private bool OnCanResolveExecute(object p) => SelectedCities.Count() > 1;
        #endregion

        #region ClearConsoleCommand
        public ClearConsoleCommand ClearConsoleCommand { get => new ClearConsoleCommand(p => OnCanClearConsoleExecute(p), p => OnConsoleClearExecuted(p)); }

        private void OnConsoleClearExecuted(object p)
        {
            var text = p as string;
            text = "";
        }

        private bool OnCanClearConsoleExecute(object p) => !string.IsNullOrEmpty(ConsoleText);
        #endregion

        #region CancelCitiesSelecting
        public CancelCitiesSelectingCommand CancelCitiesSelecting
        {
            get => new CancelCitiesSelectingCommand(p => OnCanCancelSelectingExecute(p),
                                           p => OnCancelSelectingExecuted(p));
        }

        private void OnCancelSelectingExecuted(object p)
        {
            foreach (var city in SelectedCities)
            {
                SelectedCities.Remove(city);
            }
            HighLightedCity = null;
            State = StateLine.GetStatus(StateBar.PushButton);
            CanSelected = false;
        }

        private bool OnCanCancelSelectingExecute(object p) => SelectedCities.Count > 0;
        #endregion

        public int CitiesCount() => CityCollection.Count;

        public int RoutesCount() => RouteCollection.Count;

        public bool IsRouteHasBothCities() => SelectedRoute.FirstCity != null && SelectedRoute.SecondCity != null;

        public bool RouteWasSaved() => RouteCollection.Contains(SelectedRoute);

        public bool CityWasSaved() => CityCollection.Contains(SelectedCity);
    }
}
