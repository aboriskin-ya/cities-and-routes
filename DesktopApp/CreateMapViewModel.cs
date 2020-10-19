using DesktopApp.Service;
using DesktopApp.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GongSolutions.Wpf.DragDrop;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopApp
{
    public enum AllowExtensions { jpg, png };
    public class CreateMapViewModel : ViewModelBase, IDropTarget
    {

        private string mapName;
        private string mapPath;
        private bool isAvailableForDownload;

        private readonly IMessageBoxService _messageBoxService;
        public CreateMapViewModel(IMessageBoxService messageBoxService)
        {
            _messageBoxService = messageBoxService;
            InitializeProperties();
            CreateCommand = new RelayCommand(
                p => CreateNewMap(), 
                b => { return !string.IsNullOrEmpty(MapName) && IsAvailableForDownload; });
        }

        public void InitializeProperties()
        {
            MapName = "";
            MapPath = "/Resources/Icons/uploadIcon.png";
            isAvailableForDownload = false;
        }
        public bool IsAvailableForDownload
        {
            get { return isAvailableForDownload; }
            set
            {
                isAvailableForDownload = value;
                RaisePropertyChanged("IsAvailableForDownload");
            }
        }
        public string MapPath
        {
            get { return mapPath; }
            set
            {
                mapPath = value;
                RaisePropertyChanged("MapPath");
            }
        }
        public string MapName
        {
            get { return mapName; }
            set
            {
                mapName = value;
                RaisePropertyChanged("MapName");
            }            
        }

        public ICommand CreateCommand { get; }
        private async void CreateNewMap()
        {
            ImageAPIService service = new ImageAPIService();
            var res = await service.UploadImage(MapPath);

            if (res != null)
            {
                _messageBoxService.Show($"We have new map \"{MapName}\" with id = {res}", "Success");
                InitializeProperties();
            }
            else
                _messageBoxService.Show("Oops! Some server problems", "Error");
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
                    MapName = Path.GetFileNameWithoutExtension(dropPath[0]);
                    MapPath = fullPath;
                    isAvailableForDownload = true;
                }
                else
                    _messageBoxService.Show($"{Path.GetFileName(dropPath[0])} has an inappropriate format or resolution", "Error");
            }
            catch(Exception ex)
            {
                _messageBoxService.Show($"Error:\n {ex}", "Error");
            }
        }

    }
}