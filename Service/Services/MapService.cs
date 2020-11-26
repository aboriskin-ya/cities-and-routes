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
    public class MapService : IMapService
    {
        private readonly IMapRepository _repository;
        protected readonly CityRouteContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<MapService> _logger;

        public MapService(IMapRepository repository, CityRouteContext context, IMapper mapper, ILogger<MapService> logger)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public MapGetDTO CreateMap(MapCreateDTO dto)
        {
            _logger.LogInformation("Create map started");
            var map = _mapper.Map<Map>(dto);
            _repository.Add(map);
            _context.SaveChanges();
            _logger.LogInformation("Create map finished");
            return _mapper.Map<MapGetDTO>(map);
        }

        public IEnumerable<MapGetDTO> GetMaps()
        {
            _logger.LogInformation("Get maps started");
            return _mapper.Map<IEnumerable<Map>, IEnumerable<MapGetDTO>>(_repository.GetAll());
        }

        public IEnumerable<MapIdNameGetDTO> GetMapsNames()
        {
            return _mapper.Map<IEnumerable<Map>, IEnumerable<MapIdNameGetDTO>>(_repository.GetAll());
        }

        public MapGetDTO GetMap(Guid id)
        {
            _logger.LogInformation("Get map started");
            return _mapper.Map<Map, MapGetDTO>(_repository.GetWholeMap(id));
        }

        public bool DeleteMap(Guid id)
        {
            _logger.LogInformation("Delete map started");
            bool flag = _repository.Delete(id);
            if (flag)
            {
                _context.SaveChanges();
                _logger.LogInformation("Delete map finished");
            }
            else
                _logger.LogInformation("Delete map not finished");
            return flag;
        }

        public MapGetDTO UpdateMap(MapCreateDTO dto, Guid id)
        {
            _logger.LogInformation("Update map started");
            var map = _repository.Get(id);
            _mapper.Map(dto, map);
            map = _repository.Update(map);
            _context.SaveChanges();
            _logger.LogInformation("Update map finished");
            return _mapper.Map<MapGetDTO>(map);
        }
    }
}