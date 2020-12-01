using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace DesktopApp.UserControls
{
    public partial class ConsoleControl : UserControl
    {
        public ConsoleControl()
        {
            InitializeComponent();
            SetBinding(ClearConsoleCommandProperty, new Binding("ClearConsoleCommand"));
        }
        public string ConsoleContent
        {
            get { return (string)GetValue(ConsoleContentProperty); }
            set { SetValue(ConsoleContentProperty, value); }
        }

        public static readonly DependencyProperty ConsoleContentProperty =
            DependencyProperty.Register(nameof(ConsoleContent), typeof(string), typeof(ConsoleControl));

        public ICommand ClearConsoleCommand
        {
            get { return (ICommand)GetValue(ClearConsoleCommandProperty); }
            set { SetValue(ClearConsoleCommandProperty, value); }
        }

        public static readonly DependencyProperty ClearConsoleCommandProperty =
            DependencyProperty.Register("ClearConsoleCommand", typeof(ICommand), typeof(ConsoleControl));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClearConsoleCommand.Execute(ConsoleContent);
        }
    }
}
