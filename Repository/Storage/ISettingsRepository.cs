using DataAccess.Models;
using System;

namespace Repository.Storage
{
    public interface ISettingsRepository: IRepository<Settings> 
    {
        Map GetMap(Guid id);
    }
}
