using DesktopApp.APIInteraction;
using DesktopApp.Models;
using DesktopApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    internal class ShortestPathViewModel : BaseViewModel, IShortestPathViewModel
    {
        private readonly IPathResolverAPIService _pathResolverAPIService;
        private readonly ICityAPIService _cityAPIService;
        private readonly IMessageBoxService _messageBoxService;

        public ShortestPathViewModel(IPathResolverAPIService pathResolverAPIService, ICityAPIService cityAPIService, IMessageBoxService messageBoxService)
        {
            _pathResolverAPIService = pathResolverAPIService;
            _cityAPIService = cityAPIService;
            _messageBoxService = messageBoxService;

            InitializeModels();
        }

        private ShortestPath shortestPath;
        public ShortestPath ShortestPath
        {
            get => shortestPath;
            set => Set(ref shortestPath, value, nameof(ShortestPath));
        }

        private string _consoleResult;
        public string ConsoleResult
        {
            get => _consoleResult;
            set => Set<string>(ref _consoleResult, value);
        }

        public ICommand ClearConsoleCommand { get => new RelayCommand(p => OnClearConsoleExecuted(p), p => OnCanClearExecute(p)); }

        private bool OnCanClearExecute(object p) => !string.IsNullOrEmpty(ConsoleResult);

        private void OnClearConsoleExecuted(object p)
        {
            ConsoleResult = "";
            ShortestPath.CitiesPosition.RemoveAll(t => t != null);
        }

        public ICommand CalculateShortestPathCommand => new RelayCommand(p => OnCalculateShortestPath(p), p => OnCanCalculateShortestPathExecute(p));

        private async void OnCalculateShortestPath(object p)
        {
            var path = p as PathModel;
            if (path == null)
                return;

            var res = await _pathResolverAPIService.FindShortestPathAsync(path);
            if (!res.IsSuccessful)
                _messageBoxService.ShowError("An error occured. Please try it again.", "Failed result");
            else
            {
                if (!res.Payload.IsPathFound)
                    ConsoleResult += $"There is no path from \"{path.CityFromName}\" to \"{path.CityToName}\"";
                else
                    await GetCitiesAsync(res.Payload);
            }
        }

        private async Task<City> GetCityAsync(Guid guid)
        {
            var city = await _cityAPIService.GetCityAsync(guid);
            if (!city.IsSuccessful)
                _messageBoxService.ShowError("An error occured. Please try it again.", "Failed result");
            return city.Payload;
        }

        private async Task GetCitiesAsync(ShortestPath shortestPath)
        {
            var builder = new StringBuilder();
            var cities = new List<Point>();
            ConsoleResult += "Route has forward cities: ";
            foreach (var guid in shortestPath.Path)
            {
                var city = await GetCityAsync(guid);
                if(city != null)
                {
                    cities.Add(new Point { X = city.X, Y = city.Y });
                    ConsoleResult += $"{city.Name}->";
                }
            }
            ConsoleResult = ConsoleResult.Substring(0, ConsoleResult.Length - 2);
            builder.Append($"\nProcess` duration: {shortestPath.ProcessDuration}\n" +
                            $"Calculated distance: {shortestPath.FinalDistance}\n");
            ConsoleResult += builder.ToString();
            ShortestPath.CitiesPosition = cities;
        }

        private bool OnCanCalculateShortestPathExecute(object p) => true;

        public void InitializeModels()
        {
            ShortestPath = new ShortestPath();
            ConsoleResult = "";
        }
    }
}
