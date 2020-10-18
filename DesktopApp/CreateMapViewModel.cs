using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DesktopApp
{
    public class CreateMapViewModel: ViewModelBase, IDropTarget
    {
        private string mapName = "new map";
        public string MapName
        {
            get { return mapName; }
            set
            {
                mapName = value;
                RaisePropertyChanged();
            }
        }   

        public ICommand CreateCommand { get; }

        private void CreateNewMap()
        {
            
            mapName = "fuck the system";
            RaisePropertyChanged("MapName");
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
            var dataObject = dropInfo.Data as DataObject;
            string[] dropPath = dataObject.GetData(DataFormats.FileDrop, true) as string[];

            if (Enum.IsDefined(typeof(AllowExtensions), Path.GetExtension(dropPath[0]).Trim('.')))
            {
                MapName = Path.GetFileNameWithoutExtension(dropPath[0]);
                
            }
        }

        public CreateMapViewModel()
        {
            CreateCommand = new RelayCommand(CreateNewMap);
        }
       
    }
}
