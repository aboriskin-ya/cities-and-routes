using DesktopApp.Models;
using DesktopApp.Services.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    internal interface IMapViewModel
    {
        WholeMap WholeMap { get; set; }

        ObservableCollection<City> SelectedCities { get; set; }

        City SelectedCity { get; set; }

        RelayCommandAsync CreateNewCityCommand { get; }

        ICommand UpdateCityCommand { get; }

        ICommand CancelCreatingCityCommand { get; }

        ICommand DeleteCityCommand { get; }

        ICommand CancelCreatingRouteCommand { get; }

        RelayCommandAsync CreateNewRouteCommand { get; }

        ICommand UpdateRouteCommand { get; }

        ICommand DeleteRouteCommand { get; }

        int CitiesCount();

        int RoutesCount();

        bool IsRouteHasBothCities();

        bool RouteWasSaved();

        bool CityWasSaved();

        bool IsHaveMap();

        void InitializeModels();
    }
}