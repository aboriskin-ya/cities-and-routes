using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface ISettingsService
    {
        IEnumerable<Settings> GetSettings();
        Settings GetSettings(Guid id);
        Map GetMap(Guid id);
        void CreateSettings(Settings settings);
        Settings UpdateSettings(Settings settings);
        bool DeleteSettings(Guid id);
    }
}
