using DesktopApp.APIInteraction;
using DesktopApp.Models;
using DesktopApp.Services.Commands;
using System;
using System.Collections.ObjectModel;

namespace DesktopApp.ViewModels
{
    internal class TravelSalesmanViewModel:BaseViewModel
    {
        enum ActionState
        {
            SelectCities,
            SelectMethod,
            ConfirmSelect
        }
        enum SelectedMethod
        {
            Annealing,
            Nearest,
            Quickest
        }
        private ICityAPIService _cityService;
        private IRouteAPIService _routeService;
        private MapViewModel _mapViewModel;
        public TravelSalesmanViewModel(ICityAPIService cityService)
        {
            SelectedCities = new ObservableCollection<City>();
            _cityService = cityService;
        }

        public ObservableCollection<City> SelectedCities { get; set; }
        #region SelectCityCommand
        public SelectCityCommand SelectCityCommand { get => new SelectCityCommand(p => OnCanSelectCityExecute(p), p=>OnSelectCityExecuted(p); }

        private void OnSelectCityExecuted(object p)
        {
            
        }

        private bool OnCanSelectCityExecute(object p) => true;
       
       
    }
        #endregion

    }
}
