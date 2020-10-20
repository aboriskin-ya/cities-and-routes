using DesktopApp.APIInteraction;
using DesktopApp.Service;
using System.Windows;

namespace DesktopApp
{
    public partial class CreateMapDialog : Window
    {
        public CreateMapDialog()
        {
            InitializeComponent();
            DataContext = new CreateMapViewModel(new MessageBoxService());
            APIClient.InitializeClient("http://localhost:54877/");
        }
    }
}
