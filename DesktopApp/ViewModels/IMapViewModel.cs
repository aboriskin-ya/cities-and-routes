using DesktopApp.Models;
using DesktopApp.Services.Commands;

namespace DesktopApp.ViewModels
{
    internal interface IMapViewModel
    {
        WholeMap WholeMap { get; set; }

        City SelectedCity { get; set; }

        Route SelectedRoute { get; set; }

        CreateCityCommandAsync CreateNewCityCommand { get; }

        UpdateCityCommand UpdateCityCommand { get; }

        CancelCreatingCityCommand CancelCreatingCityCommand { get; }

        DeleteCityCommand DeleteCityCommand { get; }

        CancelCreatingRouteCommand CancelCreatingRouteCommand { get; }

        CreateRouteCommandAsync CreateNewRouteCommand { get; }

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