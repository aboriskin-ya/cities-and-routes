using DesktopApp.ViewModels;
using System;
using System.Collections.Generic;
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

        private List<Point> citiesPosition = new List<Point>();
        public List<Point> CitiesPosition
        {
            get => citiesPosition;
            set => Set(ref citiesPosition, value, nameof(CitiesPosition));
        }
    }
}
