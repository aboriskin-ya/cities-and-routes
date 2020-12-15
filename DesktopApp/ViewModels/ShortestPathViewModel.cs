using DesktopApp.APIInteraction;
using DesktopApp.Models;
using DesktopApp.Services;
using DesktopApp.Services.Console;
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
            set => Set(ref _consoleResult, value);
        }

        public ICommand CancelCalculateShortestPathCommand { get => new RelayCommand(p => OnCancelCalculateShortestPathExecuted(p), p => OnCancelCalculateShortestPathExecute(p)); }

        private void OnCancelCalculateShortestPathExecuted(object p)
        {
            InitializeModels();
        }

        private bool OnCancelCalculateShortestPathExecute(object p) => true;


        public ICommand CalculateShortestPathCommand => new RelayCommand(p => OnCalculateShortestPath(p), p => OnCanCalculateShortestPathExecute(p));

        private async void OnCalculateShortestPath(object p)
        {
            ConsoleResult += ConsoleOutput.SolveButtonPressed();
            var path = p as PathModel;
            if (path == null)
                return;
            var res = await _pathResolverAPIService.FindShortestPathAsync(path);
            if (!res.IsSuccessful)
                _messageBoxService.ShowError("An error occured. Please try it again.", "Failed result");
            else
            {
                ConsoleResult += ConsoleOutput.ResultBoundary();
                if (!res.Payload.IsPathFound)
                    ConsoleResult += ConsoleOutput.FailedResult();
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
            var cities = new List<Point>();

            ConsoleResult += ConsoleOutput.SuccessfulResult(shortestPath.ProcessDuration, shortestPath.FinalDistance);

            var position = 0;
            var count = shortestPath.Path.Count;
            foreach (var guid in shortestPath.Path)
            {
                var city = await GetCityAsync(guid);
                if(city != null)
                {
                    cities.Add(new Point { X = city.X, Y = city.Y });
                    ConsoleResult += city.Name;
                    if (++position != count)
                        ConsoleResult += ConsoleOutput.Arrow();
                }
            }
            ConsoleResult += ConsoleOutput.Boundary();
            ShortestPath = shortestPath;
            ShortestPath.CitiesPosition = cities;
        }

        private bool OnCanCalculateShortestPathExecute(object p) => true;

        public void InitializeModels()
        {
            ShortestPath = new ShortestPath();
            ShortestPath.CitiesPosition = new List<Point>();
            ConsoleResult = ConsoleOutput.Empty();
        }

        public ICommand SelectCityCommand { get => new RelayCommand(p => OnSelectCityExecuted(p), p => OnCanSelectCityExecute(p)); }

        private bool OnCanSelectCityExecute(object p) => true;
        public void OnSelectCityExecuted(object p)
        {
            var name = p as string;
            ConsoleResult += ConsoleOutput.CityName(name);
        }
    }
}
