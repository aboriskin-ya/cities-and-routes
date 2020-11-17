using GalaSoft.MvvmLight;
using System;

namespace DesktopApp.Models
{
    public class City : ViewModelBase, System.ComponentModel.IDataErrorInfo
    {
        public Guid Id { get; set; }

        public Guid MapId
        {
            get
            {
                return Guid.Parse("dff6f7c0-26c7-47ad-2b04-08d889ea1d26");
            }
        }

        private double x = -100;
        public double X
        {
            get { return x; }
            set
            {
                x = value;
                RaisePropertyChanged(nameof(X));
            }
        }

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

        private double y = -100;
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                RaisePropertyChanged(nameof(Y));
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
                            error = "Name of a city cannot be empty.";
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
