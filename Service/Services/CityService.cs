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

        public CityCreateDTO CreateCity(CityCreateDTO dto)
        {
            _logger.LogInformation("City create started");
            var city = _mapper.Map<City>(dto);
            _repository.Add(city);
            _context.SaveChanges();
            _logger.LogInformation("City create finished");
            return _mapper.Map<CityCreateDTO>(city);
        }

        public IEnumerable<CityGetDTO> GetCities()
        {
            _logger.LogInformation("Get cities started");
            return _mapper.Map<IEnumerable<City>, IEnumerable<CityGetDTO>>(_repository.GetAll());
        }

        public CityGetDTO GetCity(Guid id)
        {
            _logger.LogInformation("Get city started");
            return _mapper.Map<City, CityGetDTO>(_repository.Get(id));
        }

        public bool DeleteCity(Guid id)
        {
            _logger.LogInformation("Delete city started");
            bool flag;
            if (flag = _repository.Delete(id))
            {
                _context.SaveChanges();
                _logger.LogInformation("Delete city finished");
                return flag;
            }
            else
            {
                _logger.LogInformation("Delete city not finished");
                return flag;
            }
        }

        public CityCreateDTO UpdateCity(Guid id, CityCreateDTO dto)
        {
            _logger.LogInformation("Update city started");
            var city = _repository.Get(id);
            _mapper.Map(dto, city);
            city = _repository.Update(city);
            _context.SaveChanges();
            _mapper.Map(city, dto);
            _logger.LogInformation("Update city finished");
            return dto;
        }
    }
}