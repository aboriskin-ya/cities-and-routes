using Service.DTO;
using System;
using System.Collections.Generic;

namespace Service.Services.Interfaces
{
    public interface ISettingsService
    {
        IEnumerable<SettingsGetDTO> GetSettings();
        SettingsGetDTO GetSettings(Guid id);
        SettingsGetDTO GetSettingsOfMap(Guid id);
        SettingsGetDTO CreateSettings(SettingsCreateDTO settingsDTO);
        SettingsGetDTO UpdateSettings(Guid Id, SettingsUpdateDTO settingsDTO);
        bool DeleteSettings(Guid id);
    }
}