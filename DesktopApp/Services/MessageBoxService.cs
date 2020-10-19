using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DesktopApp.Service
{
    public class MessageBoxService : IMessageBoxService
    {
        public void Show(string message, string caption = "Info", MessageBoxButton buttons = MessageBoxButton.OK)
        {
           MessageBox.Show(message, caption, buttons);
        }
    }
}
