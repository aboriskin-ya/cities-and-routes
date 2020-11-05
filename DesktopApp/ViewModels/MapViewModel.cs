using DesktopApp.APIInteraction;
using DesktopApp.Models;
using DesktopApp.Service;
using DesktopApp.Services.Commands;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopApp.ViewModels
{
    internal class MapViewModel : BaseViewModel, IMapViewModel
    {
        public ObservableCollection<City> CityCollection { get; set; }

        private readonly ICityAPIService _cityAPIService;

        private readonly IMessageBoxService _messageBoxService;

        public MapViewModel(ICityAPIService cityAPIService, IMessageBoxService messageBoxService)
        {
            CityCollection = new ObservableCollection<City>();
            SelectedCity = new City();
            SettingsMap = new Settings()
            {
                VertexColor = "#FF0000",
                VertexSize = 10
            };
            _cityAPIService = cityAPIService;
            _messageBoxService = messageBoxService;
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

        public CreateCityCommand CreateNewCityCommand { get => new CreateCityCommand(p => OnCanAddCollection(), async m => await AddCollectionAsync()); }
        private async Task AddCollectionAsync()
        {            
            try 
            {
                var res = await _cityAPIService.CreateCityAsync(SelectedCity);
            }
            catch(Exception ex)
            {
                RemoveFromCollection();
                _messageBoxService.ShowError(ex, "An error occured. Please try it again.");               
            }
            CityCollection.Add(SelectedCity);
        }
        private bool OnCanAddCollection() => true;

        public CancelCreatingCityCommand CancelCreatingCityCommand { get => new CancelCreatingCityCommand(p => OnCanRemoveFromCollection(), m => RemoveFromCollection()); }
        private void RemoveFromCollection()
        {
            CityCollection.Remove(SelectedCity);
            SelectedCity = new City();
        }
        private bool OnCanRemoveFromCollection() => true;
    }
}
