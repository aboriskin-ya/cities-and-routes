using GalaSoft.MvvmLight;
using System;

namespace DesktopApp.Models
{
    public class Settings : ViewModelBase
    {
        public Guid Id { get; set; }
        public Guid MapId { get; set; }

        #region FoundPathSize
        private double _foundPathSize;
        public double FoundPathSize
        {
            get => _foundPathSize;
            set
            {
                if (_foundPathSize == value) return;
                _foundPathSize = value;
                RaisePropertyChanged(nameof(FoundPathSize));
            }
        }
        #endregion

        #region FoundPathColor
        private string _foundPathColor;
        public string FoundPathColor
        {
            get => _foundPathColor;
            set
            {
                if (value == _foundPathColor) return;
                _foundPathColor = value;
                RaisePropertyChanged(nameof(FoundPathColor));
            }
        }
        #endregion

        #region DisplayCitiesNames
        private bool _displayCitiesNames;
        public bool DisplayCitiesNames
        {
            get => _displayCitiesNames;
            set => Set(ref _displayCitiesNames, value);
        }
        #endregion

        private bool displayingImage;
        public bool DisplayingImage
        {
            get { return displayingImage; }
            set
            {
                displayingImage = value;
                RaisePropertyChanged(nameof(DisplayingImage));
            }
        }

        private bool displayingGraph;
        public bool DisplayingGraph
        {
            get { return displayingGraph; }
            set
            {
                displayingGraph = value;
                RaisePropertyChanged(nameof(DisplayingGraph));
            }
        }

        private double vertexSize;
        public double VertexSize
        {
            get { return vertexSize; }
            set
            {
                vertexSize = value;
                RaisePropertyChanged(nameof(VertexSize));
            }
        }

        private string vertexColor;
        public string VertexColor
        {
            get { return vertexColor; }
            set
            {
                vertexColor = value;
                RaisePropertyChanged(nameof(VertexColor));
            }
        }

        private double edgeSize;
        public double EdgeSize
        {
            get { return edgeSize; }
            set
            {
                edgeSize = value;
                RaisePropertyChanged(nameof(EdgeSize));
            }
        }

        private string edgeColor;
        public string EdgeColor
        {
            get { return edgeColor; }
            set
            {
                edgeColor = value;
                RaisePropertyChanged(nameof(EdgeColor));
            }
        }

        public Settings(Settings settings)
        {
            Id = settings.Id;
            DisplayingGraph = settings.DisplayingGraph;
            DisplayingImage = settings.DisplayingImage;
            EdgeColor = settings.EdgeColor;
            EdgeSize = settings.EdgeSize;
            MapId = settings.MapId;
            VertexColor = settings.VertexColor;
            VertexSize = settings.VertexSize;
            FoundPathSize = settings.FoundPathSize;
            FoundPathColor = settings.FoundPathColor;
            DisplayCitiesNames = settings.DisplayCitiesNames;
        }

        public Settings() { DisplayingImage = true; }
    }
}