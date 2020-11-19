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
    public class CityService : ICityService
    {
        private ICityRepository _cityRepository;
        private IRouteRepository _routeRepository;
        protected readonly CityRouteContext _context;
        private readonly IMapper _mapper;

        public CityService(ICityRepository cityRepository, IRouteRepository routeRepository, CityRouteContext context, IMapper Cityper)
        {
            _cityRepository = cityRepository;
            _routeRepository = routeRepository;
            _context = context;
            _mapper = Cityper;
        }

        public CityGetDTO CreateCity(CityCreateDTO dto)
        {
            var city = _mapper.Map<City>(dto);
            _cityRepository.Add(city);
            _context.SaveChanges();

            return _mapper.Map<CityGetDTO>(city);
        }

        public IEnumerable<CityGetDTO> GetCities()
        {
            return _mapper.Map<IEnumerable<City>, IEnumerable<CityGetDTO>>(_cityRepository.GetAll());
        }

        public CityGetDTO GetCity(Guid id)
        {
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
                _context.SaveChanges();
            return flag;
        }

        public CityCreateDTO UpdateCity(Guid id, CityCreateDTO dto)
        {
            var city = _cityRepository.Get(id);
            _mapper.Map(dto, city);
            city = _cityRepository.Update(city);
            _context.SaveChanges();

            return _mapper.Map(city, dto);
        }
    }
}