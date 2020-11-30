using DesktopApp.APIInteraction;
using DesktopApp.Dialogs.Commands;
using DesktopApp.Models;
using DesktopApp.Services;
using DesktopApp.Services.EventAggregator;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

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

        private ObservableCollection<Map> mapCollection;
        public ObservableCollection<Map> MapCollection
        {
            get => mapCollection;
            set => Set(ref mapCollection, value, nameof(MapCollection));
        }

        private Map selectedMap;
        public Map SelectedMap
        {
            get => selectedMap;
            set => Set(ref selectedMap, value, nameof(SelectedMap));
        }

        #region GetAllMapCommand

        public ICommand GetAllMapCommand => new GetAllMapCommand(p => OnCanGetAllMapExecuted(p), p => OnGetAllMapExecuted(p));

        private async void OnGetAllMapExecuted(object p)
        {
            var res = await _mapAPIService.GetAllNamesMapAsync();
            if (!res.IsSuccessful)
                _messageBoxService.ShowError("An error occured. Please try it again.", "Failed result");
            else
                MapCollection = new ObservableCollection<Map>(res.Payload);
        }

        private bool OnCanGetAllMapExecuted(object p) => true;

        #endregion

        #region DeleteMapCommand

        public ICommand DeleteMapCommand => new DeleteMapCommand(p => OnCanDeleteMapExecuted(p), p => OnDeleteMapExecuted(p));

        private async void OnDeleteMapExecuted(object p)
        {
            var res = await _mapAPIService.DeleteMapAsync(SelectedMap.Id);
            if (!res)
                _messageBoxService.ShowError("An error occured. Please try it again.", "Failed result");
            else
                MapCollection.Remove(SelectedMap);
        }

        private bool OnCanDeleteMapExecuted(object p) => true;

        #endregion

        #region LoadMapCommand

        public ICommand LoadMapCommand => new LoadMapCommand(p => OnCanLoadMapExecuted(p), p => OnLoadMapExecuted(p));

        private async void OnLoadMapExecuted(object p)
        {
            var res = await _mapAPIService.GetMapAsync(SelectedMap.Id);
            res.Payload.Image = new Image() { Data = await _imageAPIService.GetImageAsync(res.Payload.ImageId) };
            if (!res.IsSuccessful)
                _messageBoxService.ShowError("An error occured. Please try it again.", "Failed result");
            else
            {
                _eventAggregator.GetEvent<WholeMapSentEvent>().Publish(res.Payload);
                foreach (Window item in Application.Current.Windows)
                {
                    if (item.DataContext == this)
                    {
                        item.Close();
                    }
                }
            }
        }

        private bool OnCanLoadMapExecuted(object p) => true;

        #endregion
    }
}