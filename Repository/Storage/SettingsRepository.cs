using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Repository.Storage
{
    public class SettingsRepository: Repository<Settings>, ISettingsRepository
    {
        public SettingsRepository(CityRouteContext context): base(context)
        { }

        public Map GetMap(Guid id)
        {
            return _entity
                .Include(s => s.Map)
                    .ThenInclude(m => m.Image)
                .Where(s => s.MapId == id)
                .Select(s => s.Map)
                .FirstOrDefault();
        }
    }
}
