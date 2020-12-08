using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DesktopApp.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ToggleSwitch.xaml
    /// </summary>
    public partial class ToggleSwitch : UserControl
    {
        private SolidColorBrush _checkedColor;
        private SolidColorBrush _unchekedColorBrush;
        public ToggleSwitch()
        {
            InitializeComponent();
            _checkedColor = new SolidColorBrush(Color.FromArgb(100, 10, 224, 43));
            _unchekedColorBrush = new SolidColorBrush(Color.FromArgb(100, 186, 186, 196));
            Initialize(IsChecked);
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
                Background.Fill = _unchekedColorBrush;
                IsChecked = false;
                button.Margin = new Thickness(-620, 0, 0, 0);
            }
            else
            {
                Background.Fill = _checkedColor;
                IsChecked = true;
                button.Margin = new Thickness(620, 0, 0, 0);
            }
        }
        private void Initialize(bool isChecked)
        {
            if (isChecked)
            {
                Background.Fill = _unchekedColorBrush;
                IsChecked = false;
                button.Margin = new Thickness(-620, 0, 0, 0);
            }
            else
            {
                Background.Fill = _checkedColor;
                IsChecked = true;
                button.Margin = new Thickness(620, 0, 0, 0);
            }
        }
        
    }
}
