using DesktopApp.APIInteraction;
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
