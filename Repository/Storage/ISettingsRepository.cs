using DataAccess.Models;
using System;

namespace Repository.Storage
{
    public interface ISettingsRepository : IRepository<Settings>
    {
        Settings GetSettingsOfMap(Guid id);
    }
}
