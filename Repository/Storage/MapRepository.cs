using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Storages
{
    public class MapRepository : Repository<Map>, IMapRepository
    {
        public MapRepository(CityRouteContext context) : base(context)
        {
        }

        public new Map Get(Guid id)
        {
            DbSet<Map> entity = _context.Set<Map>();
            return entity.Include(p => p.Image).SingleOrDefault(p => p.Id == id);
        }
    }
}
