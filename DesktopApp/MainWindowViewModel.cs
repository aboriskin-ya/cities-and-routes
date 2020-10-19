using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace DesktopApp
{
    class MainWindowViewModel: ViewModelBase
    {
        public MainWindowViewModel()
        {
            ShowCreateMapDialogCommand = new RelayCommand(ShowDialog);
        }

        public ICommand ShowCreateMapDialogCommand { get; }

        private void ShowDialog()
        {
            CreateMapDialog cmd = new CreateMapDialog();
            cmd.Owner = App.Current.MainWindow;
            cmd.Show();
        }
    }
}
