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
        SettingsDTO CreateSettings(SettingsDTO settingsDTO);
        SettingsDTO UpdateSettings(Guid Id, SettingsUpdateDTO settingsDTO);
        bool DeleteSettings(Guid id);
    }
}