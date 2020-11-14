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
        private ICityRepository _repository;
        protected readonly CityRouteContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CityService> _logger;

        public CityService(ICityRepository Repository, CityRouteContext context, IMapper Cityper, ILogger<CityService> logger)
        {
            _repository = Repository;
            _context = context;
            _mapper = Cityper;
            _logger = logger;
        }

        public City CreateCity(CityDTO Dto)
        {
            _logger.LogInformation("City create started");
            City city = _mapper.Map<City>(Dto);
            _repository.Add(city);
            _context.SaveChanges();
            _logger.LogInformation("City create finished");
            return city;
        }

        public IEnumerable<CityDTO> GetCities()
        {
            _logger.LogInformation("Get cities started");
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
            _logger.LogInformation("Get city started");
            return _mapper.Map<City, CityDTO>(_repository.Get(id));
        }

        public bool DeleteCity(Guid Id)
        {
            _logger.LogInformation("Delete city started");
            bool result;
            if (result = _repository.Delete(Id))
            {
                _context.SaveChanges();
                _logger.LogInformation("Delete city finished");
                return result;
            }
            else
            {
                _logger.LogInformation("Delete city not finished");
                return result;
            }
        }

        public City UpdateCity(Guid Id, CityDTO Dto)
        {
            _logger.LogInformation("Update city started");
            City City = _repository.Get(Id);
            _mapper.Map<CityDTO, City>(Dto, City);
            City = _repository.Update(City);
            _context.SaveChanges();
            _logger.LogInformation("Update city finished");
            return City;
        }
    }
}
