using DesktopApp.APIInteraction;
using DesktopApp.Models;
using DesktopApp.Services;
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
            

            SelectedCities = new ObservableCollection<City>();
            SelectedCity = new City();
            State = StateLine.GetStatus(StateBar.PushButton);
            WholeMap = new WholeMap()
            {
                Cities = new ObservableCollection<City>(),
                Routes = new ObservableCollection<Route>(),
                Settings = new Settings()
            };
            InitializeModels();
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

        private WholeMap wholeMap;
        public WholeMap WholeMap
        {
            get => wholeMap;
            set => Set(ref wholeMap, value, nameof(WholeMap));
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
        public Route SelectedRoute;
        private City selectedCity;
        public City SelectedCity
        {
            get => selectedCity;
            set => Set(ref selectedCity, value, nameof(SelectedCity));
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

        public UpdateCityCommand UpdateCityCommand { get => new UpdateCityCommand(p => OnCanUpdateCityCollection(), async m => await OnUpdateCityCollectionAsync()); }
        private async Task OnUpdateCityCollectionAsync()
        {
            var res = await _cityAPIService.UpdateCityAsync(SelectedCity);
            if (!res.IsSuccessful)
            {
                _messageBoxService.ShowError("An error occured. Please try it again.", "Error occured");
            }
        }

        private bool OnCanUpdateCityCollection() => true;

        public CancelCreatingCityCommand CancelCreatingCityCommand { get => new CancelCreatingCityCommand(p => OnCanRemoveCityFromCollection(), m => OnRemoveCityFromCollection()); }
        private void OnRemoveCityFromCollection()
        {
            SelectedCity = new City();
        }
        private bool OnCanRemoveCityFromCollection() => true;

        public DeleteCityCommand DeleteCityCommand { get => new DeleteCityCommand(p => OnCanDeleteCityFromCollection(), async m => await OnDeleteCityFromCollection()); }
        private async Task OnDeleteCityFromCollection()
        {
            var res = await _cityAPIService.DeleteCityAsync(SelectedCity);
            if (res.IsSuccessful)
            {
                WholeMap.Cities.Remove(SelectedCity);
                SelectedCity = new City();
            }
            else
            {
                _messageBoxService.ShowError("An error occured. Please try it again.", "Error occured");
                OnRemoveCityFromCollection();
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

        public CancelCreatingRouteCommand CancelCreatingRouteCommand { get => new CancelCreatingRouteCommand(p => OnCanRemoveRouteFromCollection(), m => OnRemoveRouteFromCollection()); }
        private void OnRemoveRouteFromCollection()
        {
            SelectedRoute = new Route();
        }
        private bool OnCanRemoveRouteFromCollection() => true;

        public UpdateRouteCommand UpdateRouteCommand { get => new UpdateRouteCommand(p => OnCanUpdateRouteCollection(), async m => await OnUpdateRouteCollectionAsync()); }
        private async Task OnUpdateRouteCollectionAsync()
        {
            var res = await _routeAPIService.UpdateRouteAsync(SelectedRoute);
            if (!res.IsSuccessful)
            {
                _messageBoxService.ShowError("An error occured. Please try it again.", "Error occured");
            }
        }

        private bool OnCanUpdateRouteCollection() => true;

        public DeleteRouteCommand DeleteRouteCommand { get => new DeleteRouteCommand(p => OnCanDeleteRouteFromCollection(), async m => await OnDeleteRouteFromCollection()); }
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
                OnRemoveRouteFromCollection();
            }
        }
        private bool OnCanDeleteRouteFromCollection() => true;


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
                var city = await _cityAPIService.GetCityAsync(cityId);
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