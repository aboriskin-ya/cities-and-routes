using Autofac;
using DesktopApp.Dialogs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    class MainWindowViewModel: ViewModelBase
    {
        public MainWindowViewModel()
        {
            ShowCreateMapDialogCommand = new RelayCommand(s => ShowDialog());
        }

        public ICommand ShowCreateMapDialogCommand { get; }

        private void ShowDialog()
        {
            var model = RegisterServices.Configure().Resolve<CreateMapViewModel>();
            var view = new CreateMapDialog { DataContext = model };
            view.Owner = App.Current.MainWindow;
            view.Show();
        }
    }
}
