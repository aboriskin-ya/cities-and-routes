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

        public CityGetDTO CreateCity(CityCreateDTO dto)
        {
            var city = _mapper.Map<City>(dto);
            _repository.Add(city);
            _context.SaveChanges();

            return _mapper.Map<CityGetDTO>(city);
        }

        public IEnumerable<CityGetDTO> GetCities()
        {
            return _mapper.Map<IEnumerable<City>, IEnumerable<CityGetDTO>>(_repository.GetAll());
        }

        public CityGetDTO GetCity(Guid id)
        {
            return _mapper.Map<City, CityGetDTO>(_repository.Get(id));
        }

        public bool DeleteCity(Guid id)
        {
            bool flag = _repository.Delete(id);
            if (flag)
                _context.SaveChanges();
            return flag;
        }

        public CityCreateDTO UpdateCity(Guid id, CityCreateDTO dto)
        {
            var city = _repository.Get(id);
            _mapper.Map(dto, city);
            city = _repository.Update(city);
            _context.SaveChanges();

            _mapper.Map(city, dto);
            return dto;
        }
    }
}