using Autofac;
using DesktopApp.APIInteraction;
using DesktopApp.ViewModels;
using System.Configuration;
using System.Windows;

namespace DesktopApp
{

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            string url = ConfigurationManager.AppSettings["baseApiUrl"];
            APIClient.InitializeClient(url);

            var model = RegisterServices.Configure().Resolve<MainWindowViewModel>();
            var view = new MainWindow { DataContext = model };

            view.Show();
        }
    }
}
