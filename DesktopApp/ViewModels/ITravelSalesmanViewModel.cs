using DesktopApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    public interface ITravelSalesmanViewModel
    {
        event EventHandler WasChanged;
        ObservableCollection<City> SelectedCities { get; set; }
        ObservableCollection<Route> SelectedRoutes { get; set; }
        City SelectedCity { get; set; }
        bool CanSelectCities { get; set; }
        bool TravelsalesmanAcces { get; set; }
        int SelectedMethodIndex { get; set; }
        string ConsoleResult { get; set; }
        string State { get; set; }
        ICommand SelectCityCommand { get; }
        ICommand CancelSelectCitiesCommand { get; }
        ICommand ResolveTravelSalesmanCommand { get; }
        ICommand ClearConsoleCommand { get; }
        int CitiesCount { get; }
        void Initialize();
    }
}
