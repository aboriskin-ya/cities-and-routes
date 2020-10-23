using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Repository.Storages
{ 
    public class RouteRepository : Repository<Route>, IRouteRepository
    {
        public RouteRepository(CityRouteContext context) : base(context)
        {
        }

        public new Route Get(Guid id)
        {
            return _entity.Include(p => p.Map).SingleOrDefault(p => p.Id == id);
        }
    }
}
