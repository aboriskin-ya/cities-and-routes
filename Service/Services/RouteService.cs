using AutoMapper;
using DataAccess.Models;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Storage;
using Service.DTO;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Service.Services
{
    class RouteService : IRouteService
    {
        public IMapper _mapper;
        private IRouteRepository _repository;
        protected readonly CityRouteContext _context;
        private readonly ILogger<RouteService> _logger;

        public RouteService(IRouteRepository repository, CityRouteContext context, IMapper Mapper, ILogger<RouteService> logger)
        {
            _mapper = Mapper;
            _repository = repository;
            _context = context;
            _logger = logger;
        }

        public IEnumerable<RouteDTO> GetRoutes()
        {
            _logger.LogInformation("Get routes started");
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
            _logger.LogInformation("Get rout started");
            return _mapper.Map<Route, RouteDTO>(_repository.Get(id));
        }

        public Route CreateRoute(RouteDTO dto)
        {
            _logger.LogInformation("Create route started");
            Route route = _mapper.Map<Route>(dto);
            _repository.Add(route);
            _context.SaveChanges();
            _logger.LogInformation("Create route finished");
            return route;
        }

        public Route UpdateRoute(RouteDTO dto, Guid id)
        {
            _logger.LogInformation("Update route started");
            Route route = _repository.Get(id);
            _mapper.Map<RouteDTO, Route>(dto, route);
            route = _repository.Update(route);
            _context.SaveChanges();
            _logger.LogInformation("Update route finished");
            return route;
        }

        public bool DeleteRoute(Guid id)
        {
            _logger.LogInformation("Delete route started");
            bool result;
            if (result = _repository.Delete(id))
            {
                _logger.LogInformation("Delete route finished");
                _context.SaveChanges();
                return result;
            }
            else
            {
                _logger.LogInformation("Delete route not finished");
                return result;
            }
        }
    }
}
