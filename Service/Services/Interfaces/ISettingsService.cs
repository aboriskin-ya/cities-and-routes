using Service.DTO;
using System;
using System.Collections.Generic;

namespace Service.Services.Interfaces
{
    public interface ISettingsService
    {
        IEnumerable<SettingsDTO> GetSettings();
        SettingsDTO GetSettings(Guid id);
        SettingsDTO GetSettingsOfMap(Guid id);
        void CreateSettings(SettingsDTO settingsDTO);
        SettingsDTO UpdateSettings(SettingsDTO settingsDTO);
        bool DeleteSettings(Guid id);
    }
}
