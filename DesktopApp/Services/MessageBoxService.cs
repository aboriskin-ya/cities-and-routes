using System;
using System.Windows;

namespace DesktopApp.Service
{
    public class MessageBoxService : IMessageBoxService
    {
        public void ShowError(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void ShowError(Exception ex, string message)
        {
            MessageBox.Show($"{message}\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowInfo(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
