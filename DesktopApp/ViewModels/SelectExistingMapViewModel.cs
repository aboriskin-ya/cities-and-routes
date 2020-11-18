using DesktopApp.APIInteraction;
using DesktopApp.Dialogs.Commands;
using DesktopApp.Models;
using DesktopApp.Services;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    class SelectExistingMapViewModel : BaseViewModel
    {
        private readonly IMapAPIService _mapAPIService;
        private readonly IImageAPIService _imageAPIService;
        private readonly IMessageBoxService _messageBoxService;

        public SelectExistingMapViewModel(IMapAPIService mapAPIService, IImageAPIService imageAPIService, IMessageBoxService messageBoxService)
        {
            _mapAPIService = mapAPIService;
            _imageAPIService = imageAPIService;
            _messageBoxService = messageBoxService;
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
            try
            {
                var res = await _mapAPIService.GetAllNamesMapAsync();
                if (!res.IsSuccessful)
                    throw new Exception();
                MapCollection = new ObservableCollection<Map>(res.Payload);
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex, "An error occured. Please try it again.");
            }
        }

        private bool OnCanGetAllMapExecuted(object p) => true;

        #endregion

        #region DeleteMapCommand

        public ICommand DeleteMapCommand => new DeleteMapCommand(p => OnCanDeleteMapExecuted(p), p => OnDeleteMapExecuted(p));

        private async void OnDeleteMapExecuted(object p)
        {
            try
            {
                var res = await _mapAPIService.DeleteMapAsync(SelectedMap.Id);
                if (!res)
                    throw new Exception();
                MapCollection.Remove(SelectedMap);
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex, "An error occured. Please try it again.");
            }
        }

        private bool OnCanDeleteMapExecuted(object p) => true;

        #endregion

        #region LoadMapCommand

        public ICommand LoadMapCommand => new LoadMapCommand(p => OnCanLoadMapExecuted(p), p => OnLoadMapExecuted(p));

        private async void OnLoadMapExecuted(object p)
        {
            try
            {
                var res = await _mapAPIService.GetMapAsync(SelectedMap.Id);
                res.Payload.Image = new Image() { Data = await _imageAPIService.GetImageAsync(res.Payload.ImageId) };
                if (!res.IsSuccessful)
                    throw new Exception();
                Messenger.Default.Send(res.Payload);
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex, "An error occured. Please try it again.");
            }
        }

        private bool OnCanLoadMapExecuted(object p) => true;

        #endregion
    }
}