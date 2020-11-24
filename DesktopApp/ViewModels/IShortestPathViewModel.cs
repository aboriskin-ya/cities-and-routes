using DesktopApp.Models;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    interface IShortestPathViewModel
    {
        ShortestPath ShortestPath { get; set; }

        ICommand ColculateShortestPathCommand { get; }

        void InitializeModels();
    }
}
