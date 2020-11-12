
using DesktopApp.Models;
using DesktopApp.Services.Commands;
using System.Collections.ObjectModel;

namespace DesktopApp.ViewModels
{
    internal interface IMapViewModel
    {
        ObservableCollection<City> CityCollection { get; set; }

        ObservableCollection<Route> RouteCollection { get; set; }

        City SelectedCity { get; set; }

        Route SelectedRoute { get; set; }

        Settings SettingsMap { get; set; }

        CreateCityCommand CreateNewCityCommand { get; }

        CancelCreatingCityCommand CancelCreatingCityCommand { get; }

        CancelCreatingRouteCommand CancelCreatingRouteCommand { get; }

        CreateRouteCommand CreateNewRouteCommand { get; }

        int CitiesCount();

        int RoutesCount();

        bool IsRouteHasBothCities();

        bool RouteWasSaved();

        bool CityWasSaved();
    }
}