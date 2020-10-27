using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Storage;
using System;
using System.Linq;

namespace Repository.Storages
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
    }
}
