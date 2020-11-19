using GalaSoft.MvvmLight;
using System;

namespace DesktopApp.Models
{
    public class Map : ViewModelBase, System.ComponentModel.IDataErrorInfo
    {
        public Guid Id { get; set; }

        public Guid ImageId { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case nameof(Name):
                        if (string.IsNullOrWhiteSpace(Name))
                            error = "Name of a map cannot be empty.";
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { return null; }
        }
    }
}