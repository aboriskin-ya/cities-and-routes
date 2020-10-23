using DataAccess.Models;
using Repository.Storages;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    class RouteService : IRouteService
    {
        private IRouteRepository _repository;
        protected readonly CityRouteContext _context;

        public RouteService(IRouteRepository repository, CityRouteContext context)
        {
            _repository = repository;
            _context = context;
        }

        public IEnumerable<Route> GetRoute()
        {
            return _repository.GetAll();
        }

        public Route GetRoute(Guid id)
        {
            return _repository.Get(id);
        }

        public void CreateRoute(Route route)
        {
            _repository.Add(route);
            _context.SaveChanges();
        }

        public Route UpdateRoute(Route route)
        {
            route = _repository.Update(route);
            _context.SaveChanges();
            return route;
        }

        public bool DeleteRoute(Guid id)
        {
            bool result;
            if (result = _repository.Delete(id))
            {
                _context.SaveChanges();
                return result;
            }
            else
            {
                return result;
            }
        }
    }
}
