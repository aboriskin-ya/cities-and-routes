using DataAccess.Models;
using Repository;
using System;
using System.Collections.Generic;
using AutoMapper;
using Repository.Storage;
using Service.Services.Interfaces;
using Service.DTO;

namespace Service.Services
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

        public IEnumerable<RouteDTO> GetRoutes()
        {
            List<RouteDTO> routeDTOs = new List<RouteDTO>();
            RouteDTO routeDTOTemp = new RouteDTO();
            foreach (var item in _repository.GetAll())
            {
                _mapper.Map<Route, RouteDTO>(item, routeDTOTemp);
                routeDTOs.Add(routeDTOTemp);
            }
            return routeDTOs;
        }

        public RouteDTO GetRoute(Guid id)
        {
            return _mapper.Map<Route, RouteDTO>(_repository.Get(id));
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
