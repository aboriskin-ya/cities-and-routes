using DesktopApp.APIInteraction;
using DesktopApp.Services.Console;
using DesktopApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    internal class TravelSalesmanViewModel : BaseViewModel, ITravelSalesmanViewModel
    {
        ICityAPIService _cityService;
        ITravelSalesmanService _travelSalesmanService;
        IMapAPIService _mapService;
        public event EventHandler WasChanged;
        public TravelSalesmanViewModel(ICityAPIService cityService, ITravelSalesmanService travelSalesmanService, IMapAPIService mapService)
        {
            _mapService = mapService;
            _cityService = cityService;
            _travelSalesmanService = travelSalesmanService;
            SelectedCities = new ObservableCollection<City>();
            SelectedRoutes = new ObservableCollection<Route>();
            Initialize();
        }

        public ObservableCollection<City> SelectedCities { get; set; }

        private ObservableCollection<Route> selecteRoutes;
        public ObservableCollection<Route> SelectedRoutes
        {
            get => selecteRoutes;
            set => Set(ref selecteRoutes, value, nameof(SelectedRoutes));
        }

        #region ConsoleResult
        private string _consoleResult;
        public string ConsoleResult
        {
            get => _consoleResult;
            set => Set<string>(ref _consoleResult, value);
        }
        #endregion

        #region HighlightedCity
        private City _selectedCity;
        public City SelectedCity
        {
            get => _selectedCity;
            set => Set<City>(ref _selectedCity, value);
        }
        #endregion

        #region CanSelectCities
        private bool _canSelect;
        public bool CanSelectCities
        {
            get => _canSelect;
            set
            {
                Set<bool>(ref _canSelect, value);
                WasChanged?.Invoke(this, new EventArgs());
            }
        }
        #endregion

        #region TravelsalesmanAcces
        private bool _travelsalesmanAcces = false;
        public bool TravelsalesmanAcces
        {
            get => _travelsalesmanAcces;
            set => Set<bool>(ref _travelsalesmanAcces, value);
        }
        #endregion

        #region SelectedMethodIndex
        private int _selectedMethodIndex;
        public int SelectedMethodIndex
        {
            get => _selectedMethodIndex;
            set => Set<int>(ref _selectedMethodIndex, value);
        }
        #endregion

        #region SelectCity
        public ICommand SelectCityCommand { get => new RelayCommand(p => OnSelectCityExecuted(p), p => OnCanSelectCityExecute(p)); }

        private bool OnCanSelectCityExecute(object p) => CanSelectCities;

        private void OnSelectCityExecuted(object p)
        {
            if (p is City)
                SelectedCity = (City)p;
            if (SelectedCities.Contains(SelectedCity)) return;
            SelectedCities.Add(SelectedCity);
            ConsoleResult += ConsoleOutput.CityName(SelectedCity.Name);
        }
        #endregion

        #region CancelSelectCitities
        public ICommand CancelSelectCitiesCommand { get => new RelayCommand(p => OnCancelSelectExecuted(p), p => OnCanCancelSelectCityExecute(p)); }

        private bool OnCanCancelSelectCityExecute(object p) => true;

        public void OnCancelSelectExecuted(object p = null)
        {            
            SelectedCity = null;
            CanSelectCities = false;            
            RemoveCities();
            RemoveRoutes();
            ClearConsole();
        }
        #endregion

        #region ResolveTravelsalesman
        public ICommand ResolveTravelSalesmanCommand { get => new RelayCommand(async p => await OnResolveExecuted(p), p => OnCanResolveExecute(p)); }

        private async Task OnResolveExecuted(object p)
        {
            RemoveRoutes();
            ConsoleResult += ConsoleOutput.SolveButtonPressed();
            
            var request = new TravelSalesmanModel()
            {
                MapId = SelectedCities.First().MapId,
                SelectedCities = SelectedCities.Select(t => t.Id).ToList()
            };
            ConsoleResult += ConsoleOutput.ResultBoundary();

            var builder = new StringBuilder();
            var model = await _travelSalesmanService.Resolve(request, SelectedMethodIndex);
            if (!model.IsSuccessful)
            {
                ConsoleResult += ConsoleOutput.FailedResult();
                RemoveCities();
            }
            else
            {                
                var payload = model.Payload;
                ConsoleResult += ConsoleOutput.SuccessfulResult(payload.NameAlghorithm, payload.ProcessDuration, payload.CalculatedDistance.ToString());

                var position = 0;
                var count = payload.PreferableSequenceOfCities.Count();               
                foreach (var cityId in payload.PreferableSequenceOfCities)
                {
                    var city = await _cityService.GetCityAsync(cityId);
                    builder.Append(city.Payload.Name);
                    if (++position != count)
                        builder.Append(ConsoleOutput.Arrow());                   
                }
                await GetSelectedRoutes(model.Payload.PreferableSequenceOfCities.ToArray(), request.MapId);
            }
            ConsoleResult += builder.ToString() + ConsoleOutput.Boundary();
        }

        private bool OnCanResolveExecute(object p) => true;
        #endregion        

        public void ClearConsole()
        {
            ConsoleResult = ConsoleOutput.Empty();
        }

        public int CitiesCount { get => SelectedCities.Count; }
        public void Initialize()
        {
            SelectedMethodIndex = 0;
            SelectedCity = new City();
            SelectedCities = new ObservableCollection<City>();
            SelectedRoutes = new ObservableCollection<Route>();
            this.OnCancelSelectExecuted(null);
        }
        private void RemoveCities()
        {
            SelectedCities = new ObservableCollection<City>();
            SelectedCity = new City();
        }
        private void RemoveRoutes()
        {
            while (SelectedRoutes.Count != 0)
            {
                var route = SelectedRoutes.First();
                SelectedRoutes.Remove(route);
            }
        }
        private async Task GetSelectedRoutes(Guid[] selectedCities, Guid mapId)
        {
            var map = await _mapService.GetMapAsync(mapId);
            var routes = map.Payload.Routes;
            for (int i = 0; i < selectedCities.Length - 1; i++)
            {
                var route = routes.FirstOrDefault(t => t.FirstCity.Id == selectedCities[i] &&
                                                     t.SecondCity.Id == selectedCities[i + 1]);
                if (route != null) SelectedRoutes.Add(route);
                else
                {
                    route = routes.FirstOrDefault(t => t.SecondCity.Id == selectedCities[i] &&
                                                        t.FirstCity.Id == selectedCities[i + 1]);
                    if (route != null) SelectedRoutes.Add(route);
                }
            }
            var lastRoute = routes.FirstOrDefault(t => t.FirstCity.Id == selectedCities[selectedCities.Length - 1] &&
                                                     t.SecondCity.Id == selectedCities[0]);
            if (lastRoute != null) SelectedRoutes.Add(lastRoute);
            else
            {
                lastRoute = routes.FirstOrDefault(t => t.FirstCity.Id == selectedCities[0] &&
                                                     t.SecondCity.Id == selectedCities[selectedCities.Length - 1]);
                SelectedRoutes.Add(lastRoute);
            }
        }
    }
}
