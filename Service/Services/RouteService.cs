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
    public class RouteService : IRouteService
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

        public IEnumerable<RouteGetDTO> GetRoutes()
        {
            _logger.LogInformation("Get routes started");
            return _mapper.Map<IEnumerable<Route>, IEnumerable<RouteGetDTO>>(_repository.GetAll());
        }

        public RouteGetDTO GetRoute(Guid id)
        {
            _logger.LogInformation("Get rout started");
            return _mapper.Map<Route, RouteGetDTO>(_repository.Get(id));
        }

        public RouteGetDTO CreateRoute(RouteCreateDTO dto)
        {
            _logger.LogInformation("Create route started");
            var route = _mapper.Map<Route>(dto);
            _repository.Add(route);
            _context.SaveChanges();
            _logger.LogInformation("Create route finished");
            return _mapper.Map<Route, RouteGetDTO>(_repository.Get(route.Id));
        }

        public RouteGetDTO UpdateRoute(Guid id, RouteCreateDTO dto)
        {
            _logger.LogInformation("Update route started");
            var route = _repository.Get(id);
            _mapper.Map(dto, route);
            route = _repository.Update(route);
            _context.SaveChanges();
            _logger.LogInformation("Update route finished");
            return _mapper.Map<RouteGetDTO>(route);
        }

        public bool DeleteRoute(Guid id)
        {
            _logger.LogInformation("Delete route started");
            bool flag = _repository.Delete(id);
            if (flag)
            {
                _context.SaveChanges();
                _logger.LogInformation("Delete route finished");
            }
            else
                _logger.LogInformation("Delete route not finished");
            return flag;
        }
    }
}