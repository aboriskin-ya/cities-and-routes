using DesktopApp.Models;
using DesktopApp.Services.State;
using System;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    interface IShortestPathViewModel
    {
        event EventHandler WasChanged;
        ShortestPath ShortestPath { get; set; }
        ICommand ClearConsoleCommand { get; }
        string ConsoleResult { get; set; }
        ICommand CalculateShortestPathCommand { get; }

        void InitializeModels();
        void StateUpdate(StateLineStatus stateLine);
    }
}
