using Autofac;
using DesktopApp.APIInteraction;
using DesktopApp.ViewModels;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopApp
{

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            string url = ConfigurationManager.AppSettings["baseApiUrl"];
            string login = ConfigurationManager.AppSettings["authenticationType"];
            string password= ConfigurationManager.AppSettings["authenticationInfo"];
            APIClient.InitializeClient(url, login, password);

            var model = RegisterServices.Configure().Resolve<MainWindowViewModel>();
            var view = new MainWindow { DataContext = model };

            view.Show();
        }
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var t = Task.Run(() => {
                var exceptionId = LogAPIService.LoggingExceptions(e.Exception.GetType().ToString(), e.Exception.Message, e.Exception.StackTrace).Result;
                if (exceptionId.IsSuccessful)
                {
                    MessageBox.Show($"Some error happened in the application. Error Id: {exceptionId.Payload.ToString()}. If that continue happening, " +
                        $"please share that Error Id with us.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Some error happened in the application, but you have no connection to the server. If that continue happening," +
                        "please share that Error Id with us.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            });
            t.Wait();
            
            e.Handled = true;
        }
    }
}
