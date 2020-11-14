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

        public Map CreateMap(MapCreateDTO dto)
        {
            _logger.LogInformation("Create map started");
            Map map = _mapper.Map<Map>(dto);
            _repository.Add(map);
            _context.SaveChanges();
            _logger.LogInformation("Create map finished");
            return map;
        }

        public IEnumerable<MapGetDTO> GetMaps()
        {
            _logger.LogInformation("Get maps started");
            List<MapGetDTO> mapGetDTOs = new List<MapGetDTO>();
            MapGetDTO mapGetTemp = new MapGetDTO();
            foreach (var item in _repository.GetAll())
            {
                _mapper.Map<Map, MapGetDTO>(item, mapGetTemp);
                mapGetDTOs.Add(mapGetTemp);
            }
            return mapGetDTOs;
        }

        public MapGetDTO GetMap(Guid id)
        {
            _logger.LogInformation("Get map started");
            return _mapper.Map<Map, MapGetDTO>(_repository.GetWholeMap(id));
        }

        public bool DeleteMap(Guid id)
        {
            _logger.LogInformation("Delete map started");
            bool result;
            if (result = _repository.Delete(id))
            {
                _logger.LogInformation("Delete map finished");
                _context.SaveChanges();
                return result;
            }
            else
            {
                _logger.LogInformation("Delete map not finished");
                return result;
            }
        }

        public Map UpdateMap(MapCreateDTO dto, Guid id)
        {
            _logger.LogInformation("Update map started");
            Map map = _repository.Get(id);
            _mapper.Map<MapCreateDTO, Map>(dto, map);
            map = _repository.Update(map);
            _context.SaveChanges();
            _logger.LogInformation("Update map finished");
            return map;
        }
    }
}
