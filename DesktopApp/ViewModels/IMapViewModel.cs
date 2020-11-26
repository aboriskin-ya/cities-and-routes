using DesktopApp.Models;
using DesktopApp.Services.Commands;
using System.Collections.ObjectModel;

namespace DesktopApp.ViewModels
{
    internal interface IMapViewModel
    {
        WholeMap WholeMap { get; set; }

        bool CanSelected { get; set; }
        ObservableCollection<City> SelectedCities { get; set; }
        City HighLightedCity { get; set; }

        City SelectedCity { get; set; }

        ClearConsoleCommand ClearConsoleCommand { get; }
        SelectCityCommand SelectCityCommand { get; }
        RelayCommandAsync CreateNewCityCommand { get; }

        UpdateCityCommand UpdateCityCommand { get; }

        CancelCreatingCityCommand CancelCreatingCityCommand { get; }

        DeleteCityCommand DeleteCityCommand { get; }

        CancelCreatingRouteCommand CancelCreatingRouteCommand { get; }

        RelayCommandAsync CreateNewRouteCommand { get; }

        UpdateRouteCommand UpdateRouteCommand { get; }

        DeleteRouteCommand DeleteRouteCommand { get; }

        int CitiesCount();

        int RoutesCount();

        bool IsRouteHasBothCities();

        bool RouteWasSaved();

        bool CityWasSaved();

        bool IsHaveMap();

        void InitializeModels();
    }
}