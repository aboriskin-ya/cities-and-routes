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
    public class CityService : ICityService
    {
        private ICityRepository _cityRepository;
        private IRouteRepository _routeRepository;
        protected readonly CityRouteContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CityService> _logger;

        public CityService(ICityRepository cityRepository, IRouteRepository routeRepository, CityRouteContext context, IMapper Cityper, ILogger<CityService> logger)
        {
            _cityRepository = cityRepository;
            _routeRepository = routeRepository;
            _context = context;
            _mapper = Cityper;
            _logger = logger;
        }

        public CityGetDTO CreateCity(CityCreateDTO dto)
        {
            _logger.LogInformation("City create started");
            var city = _mapper.Map<City>(dto);
            _cityRepository.Add(city);
            _context.SaveChanges();
            _logger.LogInformation("City create finished");
            return _mapper.Map<CityGetDTO>(city);
        }

        public IEnumerable<CityGetDTO> GetCities()
        {
            _logger.LogInformation("Get cities started");
            return _mapper.Map<List<CityGetDTO>>(_cityRepository.GetAll());
        }

        public CityGetDTO GetCity(Guid id)
        {
            _logger.LogInformation("Get city started");
            return _mapper.Map<City, CityGetDTO>(_cityRepository.Get(id));
        }

        public bool DeleteCity(Guid id)
        {
            var routes = _cityRepository.GetRoutes(id);
            foreach (var route in routes)
            {
                _routeRepository.Delete(route.Id);
            }

            bool flag = _cityRepository.Delete(id);
            if (flag)
            {
                _context.SaveChanges();
                _logger.LogInformation("Delete city finished");
            }
            else
                _logger.LogInformation("Delete city not finished");
            return flag;
        }

        public CityGetDTO UpdateCity(Guid id, CityCreateDTO dto)
        {
            _logger.LogInformation("Update city started");

            var city = _cityRepository.Get(id);
            _mapper.Map(dto, city);
            city = _cityRepository.Update(city);
            _context.SaveChanges();

            _logger.LogInformation("Update city finished");
            return _mapper.Map<CityGetDTO>(city);
        }
    }
}