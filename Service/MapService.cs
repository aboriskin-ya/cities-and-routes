using DataAccess.Models;
using Repository.Storages;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class MapService : IMapService
    {
        private IMapRepository _repository;
        protected readonly CityRouteContext _context;

        public MapService(IMapRepository repository, CityRouteContext context)
        {
            _repository = repository;
            _context = context;
        }

        public void CreateMap(Map map)
        {
            _repository.Add(map);
        }

        public IEnumerable<Map> GetMap()
        {
            return _repository.GetAll();
        }

        public Map GetMap(Guid id)
        {
            return _repository.Get(id);
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

        public Map UpdateMap(Map map)
        {
            map = _repository.Update(map);
            _context.SaveChanges();
            return map;
        }
    }
}
