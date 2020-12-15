using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Storage
{
    public class MapRepository : Repository<Map>, IMapRepository
    {
        public MapRepository(CityRouteContext context) : base(context)
        {
        }

        public new Map Get(Guid id)
        {
            return _entity.Include(p => p.Image).SingleOrDefault(p => p.Id == id);
        }

        public Map GetWholeMap(Guid id)
        {
            return _entity.Include(p => p.Cities)
                .Include(p => p.Routes)
                .Include(p => p.Settings)
                .SingleOrDefault(p => p.Id == id);
        }

        public async Task<IEnumerable<MapInfo>> GetMapInfoAsync()
        {
            return await _entity.Include(p => p.Cities)
                .Include(p => p.Routes)
                .Select(p => new MapInfo
                {
                    Id = p.Id,
                    Name = p.Name,
                    CreateOnUTC = p.CreateOnUTC,
                    CountCities = p.Cities.Count(),
                    CountRoutes = p.Routes.Count()
                })
                .ToListAsync();
        }
    }
}