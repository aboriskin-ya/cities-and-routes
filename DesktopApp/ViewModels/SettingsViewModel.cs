using DesktopApp.APIInteraction;
using DesktopApp.Models;
using DesktopApp.Services;
using DesktopApp.Services.EventAggregator;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    internal class SettingsViewModel : BaseViewModel
    {
        private readonly ISettingsAPIService _settingsAPIService;
        private readonly IMessageBoxService _messageBoxService;
        private IEventAggregator _eventAggregator;
        private Settings oldSettings;

        public SettingsViewModel(ISettingsAPIService settingsAPIService, IMessageBoxService messageBoxService, IEventAggregator eventAggregator, Settings settings)
        {
            _settingsAPIService = settingsAPIService;
            _messageBoxService = messageBoxService;
            _eventAggregator = eventAggregator;
            Settings = settings;
            oldSettings = new Settings(settings);
        }

        #region CreateMapCommand

        public ICommand SaveSettingsCommand => new RelayCommand(async p => await OnSaveSettingsExecuted(p), p => OnCanSaveSettingsExecuted(p));

        private async Task OnSaveSettingsExecuted(object p)
        {
            var res = (settings.Id != default) ? await _settingsAPIService.UpdateSettingsAsync(settings)
                : await _settingsAPIService.CreateSettingsAsync(settings);
            if (!res.IsSuccessful)
                _messageBoxService.ShowError("An error occured. Please try it again.", "Failed result");
            else
            {
                _messageBoxService.ShowInfo("All changes were saved", "Success");
                oldSettings = res.Payload;
            }
            CloseWindowCommand.Execute(p);
        }

        private bool OnCanSaveSettingsExecuted(object p) => true;

        #endregion

        #region CancelCommand
        public ICommand CancelCommand => new RelayCommand(p => OnCancelExecuted(p), null);

        private void OnCancelExecuted(object p)
        {
            _eventAggregator.GetEvent<SettingsSentEvent>().Publish(oldSettings);
            CloseWindowCommand.Execute(p);
        }

        #endregion

        private Settings settings;
        public Settings Settings
        {
            get => settings;
            set => Set(ref settings, value, nameof(Settings));
        }
    }
}