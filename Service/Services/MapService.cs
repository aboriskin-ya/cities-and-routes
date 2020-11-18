using AutoMapper;
using DataAccess.Models;
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

        public MapService(IMapRepository repository, CityRouteContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        public MapGetDTO CreateMap(MapCreateDTO dto)
        {
            var map = _mapper.Map<Map>(dto);
            _repository.Add(map);
            _context.SaveChanges();

            return _mapper.Map<MapGetDTO>(map);
        }

        public IEnumerable<MapGetDTO> GetMaps()
        {
            return _mapper.Map<IEnumerable<Map>, IEnumerable<MapGetDTO>>(_repository.GetAll());
        }

        public IEnumerable<MapIdNameGetDTO> GetMapsNames()
        {
            return _mapper.Map<IEnumerable<Map>, IEnumerable<MapIdNameGetDTO>>(_repository.GetAll());
        }

        public MapGetDTO GetMap(Guid id)
        {
            return _mapper.Map<Map, MapGetDTO>(_repository.GetWholeMap(id));
        }

        public bool DeleteMap(Guid id)
        {
            bool flag = _repository.Delete(id);
            if (flag)
                _context.SaveChanges();
            return flag;
        }

        public MapCreateDTO UpdateMap(Guid id, MapCreateDTO dto)
        {
            var map = _repository.Get(id);
            _mapper.Map(dto, map);
            map = _repository.Update(map);
            _context.SaveChanges();

            return _mapper.Map(map, dto);
        }
    }
}