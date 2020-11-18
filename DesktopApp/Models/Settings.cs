using DesktopApp.Resources;
using GalaSoft.MvvmLight;

namespace DesktopApp.Models
{
    public class Settings : ViewModelBase
    {
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

        private double vertexSize = DefaultSettings.vertexSize;
        public double VertexSize
        {
            get { return vertexSize; }
            set
            {
                vertexSize = value;
                RaisePropertyChanged(nameof(VertexSize));
            }
        }

        private string vertexColor = DefaultSettings.vertexColor;
        public string VertexColor
        {
            get { return vertexColor; }
            set
            {
                vertexColor = value;
                RaisePropertyChanged(nameof(VertexColor));
            }
        }

        private double edgeSize = DefaultSettings.edgeSize;
        public double EdgeSize
        {
            get { return edgeSize; }
            set
            {
                edgeSize = value;
                RaisePropertyChanged(nameof(EdgeSize));
            }
        }

        private string edgeColor = DefaultSettings.edgeColor;
        public string EdgeColor
        {
            get { return edgeColor; }
            set
            {
                edgeColor = value;
                RaisePropertyChanged(nameof(EdgeColor));
            }
        }
    }
}