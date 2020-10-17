
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopApp
{
    class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            ShowCreateMapDialogCommand = new SimpleCommand(d => ShowDialog());
        }

        public ICommand ShowCreateMapDialogCommand { get; }

        private void ShowDialog()
        {
            CreateMapDialog cmd = new CreateMapDialog();
            cmd.Show();
        }
    }
}
