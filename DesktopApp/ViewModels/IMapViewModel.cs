
using DesktopApp.Models;
using DesktopApp.Services.Commands;
using System.Collections.ObjectModel;

namespace DesktopApp.ViewModels
{
    internal interface IMapViewModel
    {
        ObservableCollection<City> CityCollection { get; set; }

        City SelectedCity { get; set; }

        Settings SettingsMap { get; set; }

        CreateCityCommand CreateNewCityCommand { get; }

        CancelCreatingCityCommand CancelCreatingCityCommand { get; }
    }
}
