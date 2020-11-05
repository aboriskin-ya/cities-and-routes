using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Storage
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(CityRouteContext context) : base(context)
        {
        }
        public new City Get(Guid id)
        {
            return _entity.Include(p => p.Map).SingleOrDefault(p => p.Id == id);
        }

        public List<City> GetAllCityByMap(Guid mapId)
        {
            return _entity.Where(p => p.MapId == mapId).ToList();
        }
    }
}
