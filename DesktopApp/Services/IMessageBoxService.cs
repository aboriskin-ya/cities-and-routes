using System;
using System.Windows;

namespace DesktopApp.Services
{
    public interface IMessageBoxService
    {
        MessageBoxResult ShowInfo(string message, string caption, MessageBoxButton buttons = MessageBoxButton.OK);
        MessageBoxResult ShowError(Exception ex, string message, MessageBoxButton buttons = MessageBoxButton.OK);
        MessageBoxResult ShowError(string message, string caption, MessageBoxButton buttons = MessageBoxButton.OK);
        MessageBoxResult ShowConfirmation(string message, string caption, MessageBoxButton buttons);
    }
}