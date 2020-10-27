using DataAccess.Models;
using System;
using System.Linq;

namespace Repository.Storage
{
    public class SettingsRepository : Repository<Settings>, ISettingsRepository
    {
        public SettingsRepository(CityRouteContext context) : base(context)
        { }

        public Settings GetSettingsOfMap(Guid id)
        {
            return _entity
                .Where(s => s.MapId == id)
                .FirstOrDefault();
        }
    }
}
