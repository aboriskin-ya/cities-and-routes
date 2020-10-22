using DesktopApp.APIInteraction;
using DesktopApp.Service;
using DesktopApp.ViewModels;
using System.Configuration;
using System.Windows;

namespace DesktopApp.Dialogs
{
    public partial class CreateMapDialog : Window
    {
        public CreateMapDialog()
        {
            InitializeComponent();

            string url = ConfigurationManager.AppSettings["baseApiUrl"];
            APIClient.InitializeClient(url);
        }
    }
}
