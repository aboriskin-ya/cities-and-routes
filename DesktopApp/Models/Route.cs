using GalaSoft.MvvmLight;
using System;

namespace DesktopApp.Models
{
    public class Route : ViewModelBase, System.ComponentModel.IDataErrorInfo
    {
        public Guid Id { get; set; }
        public Guid MapId { get; set; }

        private City firstCity;
        public City FirstCity
        {
            get { return firstCity; }
            set
            {
                firstCity = value;
                RaisePropertyChanged(nameof(FirstCity));
            }
        }

        private City secondCity;
        public City SecondCity
        {
            get { return secondCity; }
            set
            {
                secondCity = value;
                RaisePropertyChanged(nameof(SecondCity));
            }
        }

        private double distance;
        public double Distance
        {
            get { return distance; }
            set
            {
                distance = value;
                RaisePropertyChanged(nameof(Distance));
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case nameof(Distance):
                        if ((Distance <= 0) || (Distance > 20000))
                        {
                            error = "A distance must be between 0 and 20000";
                        }
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
