using DataAccess.Models;
using Repository.Storages;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class CityService : ICityService
    {
        private ICityRepository _repository;
        protected readonly CityRouteContext _context;

        public CityService(ICityRepository repository, CityRouteContext context)
        {
            _repository = repository;
            _context = context;
        }

        public void CreateCity(City city)
        {
            _repository.Add(city);
             _context.SaveChanges();
        }

        public IEnumerable<City> GetCity()
        {
            return _repository.GetAll();
        }

        public City GetCity(Guid id)
        {
            return _repository.Get(id);
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

        public City UpdateCity(City city)
        {
            city = _repository.Update(city);
            _context.SaveChanges();
            return city;
        }
    }
}
