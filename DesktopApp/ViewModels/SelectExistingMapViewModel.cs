using DesktopApp.APIInteraction;
using DesktopApp.Models;
using DesktopApp.Services;
using DesktopApp.Services.EventAggregator;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    class SelectExistingMapViewModel : BaseViewModel
    {
        private readonly IMapAPIService _mapAPIService;
        private readonly IImageAPIService _imageAPIService;
        private readonly IMessageBoxService _messageBoxService;
        private IEventAggregator _eventAggregator;

        public SelectExistingMapViewModel(IMapAPIService mapAPIService, IImageAPIService imageAPIService, IMessageBoxService messageBoxService, IEventAggregator eventAggregator)
        {
            _mapAPIService = mapAPIService;
            _imageAPIService = imageAPIService;
            _messageBoxService = messageBoxService;
            _eventAggregator = eventAggregator;
        }

        private ObservableCollection<MapInfo> mapCollection;
        public ObservableCollection<MapInfo> MapCollection
        {
            get => mapCollection;
            set => Set(ref mapCollection, value, nameof(MapCollection));
        }

        private MapInfo selectedMap;
        public MapInfo SelectedMap
        {
            get => selectedMap;
            set => Set(ref selectedMap, value, nameof(SelectedMap));
        }

        #region GetAllMapCommand

        public ICommand GetAllMapCommand => new RelayCommand(p => OnGetAllMapExecuted(p), p => OnCanGetAllMapExecuted(p));

        private async void OnGetAllMapExecuted(object p)
        {
            var res = await _mapAPIService.GetMapInfoAsync();
            if (res.IsSuccessful == false && res.Payload == default)
            {
                _messageBoxService.ShowError("An error occured. Please try it again.", "Failed result");
                CloseWindowCommand.Execute(p);
                return;
            }
            MapCollection = new ObservableCollection<MapInfo>(res.Payload);
        }

        private bool OnCanGetAllMapExecuted(object p) => true;

        #endregion

        #region DeleteMapCommand

        public ICommand DeleteMapCommand => new RelayCommand(p => OnDeleteMapExecuted(p), p => OnCanDeleteMapExecuted(p));

        private async void OnDeleteMapExecuted(object p)
        {
            var DialogResult = _messageBoxService.ShowConfirmation($"Are you sure, you want to delete \"{SelectedMap.Name}\"?", "Confirm action", MessageBoxButton.YesNo);
            if (DialogResult == MessageBoxResult.Yes)
            {
                var res = await _mapAPIService.DeleteMapAsync(SelectedMap.Id);
                if (!res)
                    _messageBoxService.ShowError("An error occured. Please try it again.", "Failed result");
                else
                {
                    MapCollection.Remove(SelectedMap);
                }
            }
        }

        private bool OnCanDeleteMapExecuted(object p) => SelectedMap != default;

        #endregion

        #region LoadMapCommand

        public ICommand LoadMapCommand => new RelayCommand(p => OnLoadMapExecuted(p), p => OnCanLoadMapExecuted(p));

        private async void OnLoadMapExecuted(object p)
        {
            var res = await _mapAPIService.GetMapAsync(SelectedMap.Id);
            res.Payload.Image = new Image() { Data = await _imageAPIService.GetImageAsync(res.Payload.ImageId) };
            if (!res.IsSuccessful)
                _messageBoxService.ShowError("An error occured. Please try it again.", "Failed result");
            else
            {
                _eventAggregator.GetEvent<WholeMapSentEvent>().Publish(res.Payload);
                CloseWindowCommand.Execute(p);
            }
        }

        private bool OnCanLoadMapExecuted(object p) => SelectedMap != default;

        #endregion


    }
}