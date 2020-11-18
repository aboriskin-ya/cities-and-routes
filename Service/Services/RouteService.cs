using AutoMapper;
using DataAccess.Models;
using Repository;
using Repository.Storage;
using Service.DTO;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Service.Services
{
    public class RouteService : IRouteService
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

        public IEnumerable<RouteGetDTO> GetRoutes()
        {
            return _mapper.Map<IEnumerable<Route>, IEnumerable<RouteGetDTO>>(_repository.GetAll());
        }

        public RouteGetDTO GetRoute(Guid id)
        {
            return _mapper.Map<Route, RouteGetDTO>(_repository.Get(id));
        }

        public RouteGetDTO CreateRoute(RouteCreateDTO dto)
        {
            var route = _mapper.Map<Route>(dto);
            var idRoute = _repository.Add(route);
            _context.SaveChanges();

            return _mapper.Map<Route, RouteGetDTO>(_repository.Get(idRoute));
        }

        public RouteCreateDTO UpdateRoute(Guid id, RouteCreateDTO dto)
        {
            var route = _repository.Get(id);
            _mapper.Map(dto, route);
            route = _repository.Update(route);
            _context.SaveChanges();

            return _mapper.Map(route, dto);
        }

        public bool DeleteRoute(Guid id)
        {
            bool flag = _repository.Delete(id);
            if (flag)
                _context.SaveChanges();
            return flag;
        }
    }
}