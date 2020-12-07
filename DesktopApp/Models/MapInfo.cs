using GalaSoft.MvvmLight;
using System;

namespace DesktopApp.Models
{
    public class MapInfo : ViewModelBase
    {
        public Guid Id { get; set; }

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

        private int countCities;
        public int CountCities
        {
            get { return countCities; }
            set
            {
                countCities = value;
                RaisePropertyChanged(nameof(CountCities));
            }
        }

        private int countRoutes;
        public int CountRoutes
        {
            get { return countRoutes; }
            set
            {
                countRoutes = value;
                RaisePropertyChanged(nameof(CountRoutes));
            }
        }

        private DateTimeOffset createOnUTC;
        public DateTimeOffset CreateOnUTC
        {
            get { return createOnUTC; }
            set
            {
                createOnUTC = value;
                RaisePropertyChanged(nameof(CreateOnUTC));
            }
        }
    }
}