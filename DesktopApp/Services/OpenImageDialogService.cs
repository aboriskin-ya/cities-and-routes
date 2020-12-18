using Microsoft.Win32;
using System;

namespace DesktopApp.Services
{
    public class OpenImageDialogService : IOpenImageDialogService
    {
        private readonly OpenFileDialog _ofd;

        public OpenImageDialogService()
        {
            _ofd = new OpenFileDialog();
            _ofd.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
        }
        public string ShowDialog()
        {
            throw new NotImplementedException("message");
            _ofd.ShowDialog();
            return _ofd.FileName;
        }
    }
}