using DataAccess.Models;
using Repository;
using System;
using System.Collections.Generic;
using AutoMapper;
using Repository.Storage;

namespace Service
{
    class RouteService : IRouteService
    {
        public IMapper _mapper;
        private IRouteRepository _repository;
        protected readonly CityRouteContext _context;

        public RouteService(IRouteRepository repository, CityRouteContext context, IMapper Mapper)
        {
            _mapper = Mapper;
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

        public Route CreateRoute(RouteDTO dto)
        {
            Route route = _mapper.Map<Route>(dto);
            _repository.Add(route);
            _context.SaveChanges();
            return route;
        }

        public Route UpdateRoute(RouteDTO dto, Guid id)
        {
            Route route = _repository.Get(id);
            _mapper.Map<RouteDTO, Route>(dto, route);
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
