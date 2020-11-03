using DataAccess.Models;
using Repository.Storage;
using Repository;
using System;
using System.Collections.Generic;
using Service.Services.Interfaces;
using AutoMapper;
using Service.DTO;
using System.Runtime.InteropServices.ComTypes;
using System.Linq;

namespace Service.Services
{
    public class MapService : IMapService
    {
        private readonly IMapRepository _repository;
        protected readonly CityRouteContext _context;
        private readonly IMapper _mapper;

        public MapService(IMapRepository repository, CityRouteContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        public Map CreateMap(MapCreateDTO dto)
        {
            Map map = _mapper.Map<Map>(dto);
            _repository.Add(map);
            _context.SaveChanges();
            return map;
        }

        public IEnumerable<MapGetDTO> GetMaps()
        {
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
            var mapGetDTO = _mapper.Map<Map, MapGetDTO>(_repository.GetWholeMap(id));
            return mapGetDTO;
        }

        public bool DeleteMap(Guid id)
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

        public Map UpdateMap(MapCreateDTO dto, Guid id)
        {
            Map map = _repository.Get(id);
            _mapper.Map<MapCreateDTO, Map>(dto, map);
            map = _repository.Update(map);
            _context.SaveChanges();
            return map;
        }
    }
}
