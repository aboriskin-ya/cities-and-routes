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

        public CityService(ICityRepository Repository, CityRouteContext context, IMapper Cityper)
        {
            _repository = Repository;
            _context = context;
            _mapper = Cityper;
        }

        public City CreateCity(CityDTO Dto)
        {
            City city = _mapper.Map<City>(Dto);
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

        public bool DeleteCity(Guid Id)
        {
            bool result;
            if (result = _repository.Delete(Id))
            {
                _context.SaveChanges();
                return result;
            }
            else
            {
                return result;
            }
        }

        public City UpdateCity(Guid Id, CityDTO Dto)
        {
            City city = _repository.Get(id);
            _mapper.Map<CityDTO, City>(dto, city);
            city = _repository.Update(city);
            _context.SaveChanges();
            return City;
        }
    }
}
