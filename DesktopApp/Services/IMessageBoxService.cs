using System;

namespace DesktopApp.Service
{
    public interface IMessageBoxService
    {
        void ShowInfo(string message, string caption);
        void ShowError(Exception ex, string message);
        void ShowError(string message, string caption);
    }
}
