using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopApp.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ToggleSwitch.xaml
    /// </summary>
    public partial class ToggleSwitch : UserControl
    {
        public ToggleSwitch()
        {
            InitializeComponent();
        }
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(ToggleSwitch));

        public string SwitchContent
        {
            get { return (string)GetValue(SwitchContentProperty); }
            set { SetValue(SwitchContentProperty, value); }
        }
        public static readonly DependencyProperty SwitchContentProperty =
            DependencyProperty.Register("SwitchContent", typeof(string), typeof(ToggleSwitch));

        private void switch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsChecked)
            {
                IsChecked = false;
            }
            else
            {
                IsChecked = true;
            }
        }
    }
}
