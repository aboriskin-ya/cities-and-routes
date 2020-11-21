using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
