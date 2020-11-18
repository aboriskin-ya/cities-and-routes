using Microsoft.Win32;

namespace DesktopApp.Services
{
    public class OpenImageDialogService : IOpenImageDialogService
    {
        private readonly OpenFileDialog _ofd;

        public OpenImageDialogService()
        {
            _ofd = new OpenFileDialog();
            _ofd.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
        }
        public string ShowDialog()
        {
            _ofd.ShowDialog();
            return _ofd.FileName;
        }
    }
}