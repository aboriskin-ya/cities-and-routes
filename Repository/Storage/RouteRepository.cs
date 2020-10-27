using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Repository.Storage
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
