using DesktopApp.Service;
using DesktopApp.APIInteraction;
using GalaSoft.MvvmLight;
using GongSolutions.Wpf.DragDrop;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Linq.Expressions;
using System.ComponentModel;

namespace DesktopApp.ViewModels
{
    public enum AllowExtensions { jpg, png };
    public class CreateMapViewModel : INotifyPropertyChanged, IDropTarget
    {

        private string mapName;
        private string mapPath;
        private bool isAvailableForDownload;

        private readonly IMessageBoxService _messageBoxService;
        private readonly IImageAPIService _imageAPIService;
        public CreateMapViewModel(IMessageBoxService messageBoxService, IImageAPIService imageAPIService)
        {
            _messageBoxService = messageBoxService;
            _imageAPIService = imageAPIService;
            InitializeProperties();

            CreateCommand = new RelayCommand(
                p => CreateNewMap(), 
                b => { return !string.IsNullOrEmpty(MapName) && IsAvailableForDownload; });
        }

        public void InitializeProperties(string name = "", string path = "/Resources/Icons/uploadIcon.png", bool isAvailable = false)
        {
            MapName = name;
            MapPath = path;
            isAvailableForDownload = isAvailable;
        }
        public bool IsAvailableForDownload
        {
            get { return isAvailableForDownload; }
            set
            {
                isAvailableForDownload = value;
                FirePropertyChanged(p => p.IsAvailableForDownload);
            }
        }
        public string MapPath
        {
            get { return mapPath; }
            set
            {
                mapPath = value;
                FirePropertyChanged(p => p.MapPath);
            }
        }
        public string MapName
        {
            get { return mapName; }
            set
            {
                mapName = value;
                FirePropertyChanged(p => p.mapName);
            }            
        }

        public ICommand CreateCommand { get; }
        private async void CreateNewMap()
        {
            try
            {
                var res = await _imageAPIService.UploadImage(MapPath);
                if (res != null)
                {
                    _messageBoxService.ShowInfo($"We have new map \"{MapName}\" with id = {res}", "Success");
                    InitializeProperties();
                }
                else
                    _messageBoxService.ShowError("Oops! Some server problems", "Error");
            }
            catch (System.Net.Http.HttpRequestException rex)
            {
                _messageBoxService.ShowError(rex, "Server is not found.");
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex, ex.Message);
            }
            
        }

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
                    img.Width >= 1000 && img.Height >= 1000)
                {
                    InitializeProperties(Path.GetFileNameWithoutExtension(dropPath[0]), fullPath, true);
                }
                else
                {
                    dropInfo.Effects = DragDropEffects.None;
                    return;
                }
            }
            catch(Exception ex)
            {
                _messageBoxService.ShowError(ex, "Some error here:");
            }
        }
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