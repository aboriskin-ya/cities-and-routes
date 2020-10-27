using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
