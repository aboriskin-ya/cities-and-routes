using System;
using System.Windows;

namespace DesktopApp.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public MessageBoxResult ShowError(string message, string caption, MessageBoxButton buttons)
        {
            return MessageBox.Show(message, caption, buttons, MessageBoxImage.Error);
        }

        public MessageBoxResult ShowError(Exception ex, string message, MessageBoxButton buttons)
        {
            return MessageBox.Show($"{message}\n{ex.Message}", "Error", buttons, MessageBoxImage.Error);
        }

        public MessageBoxResult ShowInfo(string message, string caption, MessageBoxButton buttons)
        {
            return MessageBox.Show(message, caption, buttons, MessageBoxImage.Information);
        }

        public MessageBoxResult ShowConfirmation(string message, string caption, MessageBoxButton buttons)
        {
            return MessageBox.Show(message, caption, buttons, MessageBoxImage.Question);
        }
    }
}
