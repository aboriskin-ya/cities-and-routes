﻿using DesktopApp.APIInteraction;
using DesktopApp.Models;
using DesktopApp.Services.State;
using Service.PathResolver;
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

        public ObservableCollection<Route> SelectedRoutes { get; set; }

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
                if (value == false)
                    State = StateLine.GetStatus(StateBar.PushButton);
                else State = StateLine.GetStatus(StateBar.SelectCities);
                Set<bool>(ref _canSelect, value);
                WasChanged?.Invoke(this, new EventArgs());
            }
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

        #region CanDisplay
        private bool _canDisplay;
        public bool CanDisplay
        {
            get => _canDisplay;
            set => Set<bool>(ref _canDisplay, value);
        }
        #endregion

        #region State
        private string _state;
        public string State
        {
            get => _state;
            set => Set<string>(ref _state, value);
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
            ConsoleResult += $"{SelectedCity.Name}\n";
        }
        #endregion

        #region CancelSelectCitities
        public ICommand CancelSelectCitiesCommand { get => new RelayCommand(p => OnCancelSelectExecuted(p), p => OnCanCancelSelectCityExecute(p)); }

        private bool OnCanCancelSelectCityExecute(object p) => CitiesCount > 0;

        private void OnCancelSelectExecuted(object p)
        {
            foreach (var city in SelectedCities)
            {
                SelectedCities.Remove(city);
            }
            SelectedCity = null;
            State = StateLine.GetStatus(StateBar.PushButton);
            CanSelectCities = false;
        }
        #endregion

        #region ResolveTravelsalesman
        public ICommand ResolveTravelSalesmanCommand { get => new RelayCommand(async p => await OnResolveExecuted(p), p => OnCanResolveExecute(p)); }

        private async Task OnResolveExecuted(object p)
        {
            var request = new TravelSalesmanRequest()
            {
                MapId = SelectedCities.First().MapId,
                SelectedCities = SelectedCities.Select(t => t.Id)
            };
            State = StateLine.GetStatus(StateBar.ResolvingGoal);
            var model = await _travelSalesmanService.PostCities(request, SelectedMethodIndex);
            var builder = new StringBuilder();
            builder.Append($"\nAlgorithm: {model.Payload.NameAlghorithm}\n" +
                           $"Process` duration: {model.Payload.ProcessDuration}\n" +
                           $"Calculated distance: {model.Payload.CalculatedDistance}\n" +
                           $"Preferable sequence: ");
            foreach (var cityId in model.Payload.PreferableSequenceOfCities)
            {
                var city = await _cityService.GetCityAsync(cityId);
                builder.Append($"{city.Payload.Name} ");
            }
            await GetSelectedRoutes(model.Payload.PreferableSequenceOfCities.ToArray(), request.MapId);
            ConsoleResult += builder.ToString();
            CanDisplay = true;
            RemoveCities();
        }

        private bool OnCanResolveExecute(object p) => CitiesCount >= 2 && SelectedMethodIndex != -1;
        #endregion

        #region ClearConsoleCommand
        public ICommand ClearConsoleCommand { get => new RelayCommand(p => OnClearConsoleExecuted(p), p => OnCanClearConsoleExecute(p)); }

        private bool OnCanClearConsoleExecute(object p) => !string.IsNullOrEmpty(ConsoleResult);

        private void OnClearConsoleExecuted(object p)
        {
            ConsoleResult = "";
            CanDisplay = false;
            RemoveCities();
            RemoveRoutes();
        }
        #endregion
        public int CitiesCount { get => SelectedCities.Count; }
        private void Initialize()
        {
            State = StateLine.GetStatus(StateBar.PushButton);
            SelectedMethodIndex = -1;
            SelectedCity = new City();
            ConsoleResult = null;
        }
        private void RemoveCities()
        {
            SelectedCities = new ObservableCollection<City>();
            SelectedCity = new City();
        }
        private void RemoveRoutes()
        {
            SelectedRoutes = new ObservableCollection<Route>();
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