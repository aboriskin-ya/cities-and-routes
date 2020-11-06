using DataAccess.Models;
using Repository.Storage;
using Repository;
using System;
using System.Collections.Generic;
using AutoMapper;

namespace Service
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

        public IEnumerable<City> GetCity()
        {
            return _repository.GetAll();
        }

        public City GetCity(Guid Id)
        {
            return _repository.Get(Id);
        }

        public List<City> GetAllCityByMap(Guid MapId)
        {
            return _repository.GetAllCityByMap(MapId);
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
            City City = this.GetCity(Id);
            _mapper.Map<CityDTO, City>(Dto, City);
            City = _repository.Update(City);
            _context.SaveChanges();
            return City;
        }
    }
}
