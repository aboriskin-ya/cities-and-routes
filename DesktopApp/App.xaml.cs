using Autofac;
using DesktopApp.APIInteraction;
using DesktopApp.ViewModels;
using System;
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
        private async void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            
            var exceptionId = await LogAPIService.LoggingExceptions(e.Exception.Source, e.Exception.Message, e.Exception.StackTrace);
            MessageBox.Show($"Some error happened in the application. Error Id: {exceptionId}. If that continue happening, please share that Error Id with us.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }
    }
}
