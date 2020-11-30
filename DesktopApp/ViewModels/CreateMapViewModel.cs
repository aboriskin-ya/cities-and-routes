using DesktopApp.APIInteraction;
using DesktopApp.Dialogs.Commands;
using DesktopApp.Models;
using DesktopApp.Services;
using GongSolutions.Wpf.DragDrop;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    public enum AllowExtensions { jpg, png };
    public class CreateMapViewModel : INotifyPropertyChanged, IDropTarget
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly IOpenImageDialogService _openImageDialogService;
        private readonly IImageAPIService _imageAPIService;
        private readonly IMapAPIService _mapAPIService;

        public CreateMapViewModel(IMessageBoxService messageBoxService, IOpenImageDialogService openImageDialogService, IImageAPIService imageAPIService, IMapAPIService mapAPIService)
        {
            _messageBoxService = messageBoxService;
            _openImageDialogService = openImageDialogService;
            _imageAPIService = imageAPIService;
            _mapAPIService = mapAPIService;
            InitializeProperties();
        }

        public void InitializeProperties(string name = "", string path = "")
        {
            NewMap = new Map() { Name = name };
            MapPath = path;
        }

        private Map newMap;
        public Map NewMap
        {
            get { return newMap; }
            set
            {
                newMap = value;
                FirePropertyChanged(p => p.NewMap);
            }
        }

        private string mapPath;
        public string MapPath
        {
            get { return mapPath; }
            set
            {
                mapPath = value;
                FirePropertyChanged(p => p.MapPath);
            }
        }

        #region CreateMapCommand

        public ICommand CreateMapCommand => new CreateMapCommand(p => OnCanCreateMapExecuted(p), async p => await OnCreateMapExecuted(p));

        private async Task OnCreateMapExecuted(object p)
        {
            try
            {
                NewMap.ImageId = await AddNewImage();

                if (!AddNewMap().IsFaulted)
                {
                    _messageBoxService.ShowInfo($"We have a new map \"{NewMap.Name}\".", "Success");
                    InitializeProperties();
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex, "An error occured. Please try it again.");
            }
        }

        private async Task AddNewMap()
        {
            var res = await _mapAPIService.CreateMapAsync(NewMap);
            if (!res.IsSuccessful)
                throw new Exception("A map was not added.");
        }

        private async Task<Guid> AddNewImage()
        {
            var res = await _imageAPIService.UploadImage(MapPath);
            if (!res.IsSuccessful)
                throw new Exception("An image was not added.");
            else
                return res.Payload;
        }

        private bool OnCanCreateMapExecuted(object p) => !string.IsNullOrEmpty(NewMap.Name) && !string.IsNullOrEmpty(MapPath);

        #endregion

        #region DragDropFile

        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;

            var dataObject = dropInfo.Data as IDataObject;

            if (dataObject != null && dataObject.GetDataPresent(DataFormats.FileDrop))
            {
                dropInfo.Effects = DragDropEffects.Link;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            try
            {
                var dataObject = dropInfo.Data as DataObject;
                string[] dropPath = dataObject.GetData(DataFormats.FileDrop, true) as string[];

                string fullPath = Path.GetFullPath(dropPath[0]);

                System.Drawing.Image img = System.Drawing.Image.FromFile(fullPath);

                if (Enum.IsDefined(typeof(AllowExtensions), Path.GetExtension(dropPath[0]).Trim('.')) &&
                    img.Width >= 1000 && img.Height >= 900)
                {
                    InitializeProperties(Path.GetFileNameWithoutExtension(dropPath[0]), fullPath);
                }
                else
                {
                    dropInfo.Effects = DragDropEffects.None;
                    return;
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex, "Some error here:");
            }
        }

        #endregion

        #region OpenFileDialogCommand

        public ICommand DownloadImageCommand => new DownloadImageCommand(p => OnCanDownloadImageExecuted(p), p => OnDownloadImageExecuted(p));

        private void OnDownloadImageExecuted(object p)
        {
            var res = _openImageDialogService.ShowDialog();
            if (res != null)
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(res);
                InitializeProperties(Path.GetFileNameWithoutExtension(res), res);
            }
        }

        private bool OnCanDownloadImageExecuted(object p) => true;

        #endregion

        private void FirePropertyChanged<TValue>(Expression<Func<CreateMapViewModel, TValue>> propertySelector)
        {
            if (PropertyChanged == null)
                return;

            var memberExpression = propertySelector.Body as MemberExpression;
            if (memberExpression == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}