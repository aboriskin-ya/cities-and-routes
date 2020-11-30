using System.Windows;
using System.Windows.Controls;

namespace DesktopApp.Dialogs
{
    /// <summary>
    /// Interaction logic for SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : Page
    {
        public SettingsDialog()
        {
            InitializeComponent();
        }

        private void sldLineUp_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var itemContent = (sldLineUp.Value).ToString();
            ellips.Height = int.Parse(itemContent) * 3;
            ellips.Width = int.Parse(itemContent) * 3;
        }

        private void sldLine_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var itemContent = (sldLineDown.Value).ToString();
            lineColor.StrokeThickness = int.Parse(itemContent) * 1;

        }
    }
}
