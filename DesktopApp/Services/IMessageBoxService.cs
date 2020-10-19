using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DesktopApp.Service
{
    public interface IMessageBoxService
    {
        void Show(string message, string caption, MessageBoxButton buttons = MessageBoxButton.OK);
    }
}
