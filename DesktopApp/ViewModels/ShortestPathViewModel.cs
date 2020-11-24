using DesktopApp.APIInteraction;
using DesktopApp.Models;
using DesktopApp.Services;
using DesktopApp.Services.Commands;
using System.Collections.Generic;
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

        public ICommand ColculateShortestPathCommand => new ColculateShortestPathCommand(p => OnCanColculateShortestPathExecute(p), p => OnColculateShortestPath(p));

        private async void OnColculateShortestPath(object p)
        {
            var path = p as PathModel;
            if (path == null)
                return;

            var res = await _pathResolverAPIService.FindShortestPathAsync(path);
            if (!res.IsSuccessful)
                _messageBoxService.ShowError("An error occured. Please try it again.", "Failed result");
            else
            {
                await GetCitiesAsync(res.Payload);
            }            
        }

        private async Task GetCitiesAsync(ShortestPath shortestPath)
        {
            var cities = new List<Point>();
            foreach (var guid in shortestPath.Path)
            {
                var city = await _cityAPIService.GetCityAsync(guid);
                if (!city.IsSuccessful)
                    _messageBoxService.ShowError("An error occured. Please try it again.", "Failed result");
                else
                    cities.Add(new Point { X = city.Payload.X, Y = city.Payload.Y });
            }
            ShortestPath.CitiesPosition = cities;
        }

        private bool OnCanColculateShortestPathExecute(object p) => true;

        public void InitializeModels()
        {
            ShortestPath = new ShortestPath();
        }
    }
}
