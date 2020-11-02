using DesktopApp.APIInteraction;
using DesktopApp.Models;
using DesktopApp.Services.Commands;
using System;
using System.Collections.ObjectModel;

namespace DesktopApp.ViewModels
{
    internal class MapViewModel : BaseViewModel, IMapViewModel
    {
        public ObservableCollection<City> CityCollection { get; set; }

        private readonly ICityAPIService _cityAPIService;

        public MapViewModel(ICityAPIService cityAPIService)
        {
            CityCollection = new ObservableCollection<City>();
            SelectedCity = new City() { X = -100, Y = -100 };
            SettingsMap = new Settings()
            {
                VertexColor = "#FF0000",
                VertexSize = 10
            };
            _cityAPIService = cityAPIService;
        }

        private City _SelectedCity;
        public City SelectedCity
        {
            get => _SelectedCity;
            set => Set<City>(ref _SelectedCity, value, nameof(SelectedCity));
        }

        private Settings _SettingsMap;
        public Settings SettingsMap
        {
            get => _SettingsMap;
            set => Set<Settings>(ref _SettingsMap, value, nameof(SettingsMap));
        }        

        public CreateCityCommand CreateNewCityCommand { get => new CreateCityCommand(p => OnCanAddCollection(), m => AddCollection()); }
        private void AddCollection()
        {
            var res = _cityAPIService.CreateCityAsync(SelectedCity);
            CityCollection.Add(SelectedCity);
        }
        private bool OnCanAddCollection() => SelectedCity.Name != null;
    }
}
