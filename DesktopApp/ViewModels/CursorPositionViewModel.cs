namespace DesktopApp.ViewModels
{
    internal class CursorPositionViewModel : BaseViewModel, ICursorPositionViewModel
    {
        private double _panelX;
        private double _panelY;
        public double PanelX
        {
            get => _panelX;
            set => Set<double>(ref _panelX, value);
        }

        public double PanelY
        {
            get => _panelY;
            set => Set<double>(ref _panelY, value);
        }
    }
}
