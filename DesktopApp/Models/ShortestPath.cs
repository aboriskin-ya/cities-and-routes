using DesktopApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace DesktopApp.Models
{
    class ShortestPath : BaseViewModel
    {
        public List<Guid> Path { get; set; }

        private string finalDistance;
        public string FinalDistance
        {
            get => finalDistance;
            set => Set(ref finalDistance, value, nameof(FinalDistance));
        }

        private string processDuration;
        public string ProcessDuration
        {
            get => processDuration;
            set => Set(ref processDuration, value, nameof(ProcessDuration));
        }

        public ObservableCollection<Point> CitiesPosition { get; set; } = new ObservableCollection<Point>();
    }
}
