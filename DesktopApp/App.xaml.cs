using Autofac;
using DesktopApp.ViewModels;
using System.Windows;

namespace DesktopApp
{

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = RegisterServices.Configure();

            var model = container.Resolve<MainWindowViewModel>();
            var view = new MainWindow { DataContext = model };
            view.Show();
        }
    }
}
