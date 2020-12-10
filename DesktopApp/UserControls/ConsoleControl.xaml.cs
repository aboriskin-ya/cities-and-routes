using System.Windows;
using System.Windows.Controls;

namespace DesktopApp.UserControls
{
    public partial class ConsoleControl : UserControl
    {
        public ConsoleControl()
        {
            InitializeComponent();
        }
        public string ConsoleContent
        {
            get { return (string)GetValue(ConsoleContentProperty); }
            set { SetValue(ConsoleContentProperty, value); }
        }

        public static readonly DependencyProperty ConsoleContentProperty =
            DependencyProperty.Register(nameof(ConsoleContent), typeof(string), typeof(ConsoleControl));
    }
}
