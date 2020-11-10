using GalaSoft.MvvmLight;

namespace DesktopApp.Models
{
    public class City : ViewModelBase
    {
        private string name;
        private double x = -100;
        private double y = -100;
        public double X
        {
            get { return x; }
            set
            {
                x = value;
                RaisePropertyChanged(nameof(X));
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                RaisePropertyChanged(nameof(Y));
            }
        }

    }
}
