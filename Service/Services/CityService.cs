using DataAccess.Models;
using Repository.Storage;
using Repository;
using System;
using System.Collections.Generic;
using AutoMapper;
using Service.Services.Interfaces;
using Service.DTO;

namespace Service.Services
{
    public class CityService : ICityService
    {
        private ICityRepository _repository;
        protected readonly CityRouteContext _context;
        private readonly IMapper _mapper;

        public CityService(ICityRepository repository, CityRouteContext context, IMapper Cityper)
        {
            _repository = repository;
            _context = context;
            _mapper = Cityper;
        }

        public City CreateCity(CityDTO dto)
        {
            City city = _mapper.Map<City>(dto);
            _repository.Add(city);
            _context.SaveChanges();
            return city;
        }

        public IEnumerable<CityDTO> GetCities()
        {
            List<CityDTO> cityDTOs = new List<CityDTO>();
            CityDTO cityDTOTemp = new CityDTO();
            foreach (var item in _repository.GetAll())
            {
                _mapper.Map<City, CityDTO>(item, cityDTOTemp);
                cityDTOs.Add(cityDTOTemp);
            }
            return cityDTOs;
        }

        public CityDTO GetCity(Guid id)
        {
            return _mapper.Map<City, CityDTO>(_repository.Get(id));
        }

        public List<City> GetAllCityByMap(Guid mapId)
        {
            return _repository.GetAllCityByMap(mapId);
        }

        public bool DeleteCity(Guid id)
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

        public City UpdateCity(Guid id, CityDTO dto)
        {
            City city = _repository.Get(id);
            _mapper.Map<CityDTO, City>(dto, city);
            city = _repository.Update(city);
            _context.SaveChanges();
            return city;
        }
    }
}
