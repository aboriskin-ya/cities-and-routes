using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;

namespace DesktopApp.Models
{
    public class WholeMap : ViewModelBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ImageId { get; set; }
        public Image Image { get; set; }

        private ObservableCollection<City> cities;
        public ObservableCollection<City> Cities
        {
            get => cities;
            set => Set(ref cities, value, nameof(Cities));
        }

        private ObservableCollection<Route> routes;
        public ObservableCollection<Route> Routes
        {
            get => routes;
            set => Set(ref routes, value, nameof(Routes));
        }

        private Settings settings;
        public Settings Settings
        {
            get => settings;
            set => Set(ref settings, value, nameof(Settings));
        }
    }
}