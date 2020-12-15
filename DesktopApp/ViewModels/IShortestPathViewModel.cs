using DesktopApp.Models;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    interface IShortestPathViewModel
    {
        ShortestPath ShortestPath { get; set; }
        ICommand CancelCalculateShortestPathCommand { get; }
        string ConsoleResult { get; set; }
        ICommand CalculateShortestPathCommand { get; }
        ICommand SelectCityCommand { get; }
        void InitializeModels();
    }
}
