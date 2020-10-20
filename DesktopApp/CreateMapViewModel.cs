using DesktopApp.Service;
using DesktopApp.APIInteraction;
using GalaSoft.MvvmLight;
using GongSolutions.Wpf.DragDrop;
using System;
using System.IO;
using System.Windows;
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
            try
            {
                var res = await service.UploadImage(MapPath);
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
                    MapName = Path.GetFileNameWithoutExtension(dropPath[0]);
                    MapPath = fullPath;
                    isAvailableForDownload = true;
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

    }
}