using DataAccess.Models;
using Repository.Storages;
using System;
using System.Collections.Generic;

namespace Service
{
    public class MapService : IMapService
    {
        private IRepository<Map> _repository;

        public MapService(IRepository<Map> repository)
        {
            _repository = repository;
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
            return _repository.Delete(id);
        }

        public Map UpdateMap(Map map)
        {
            return _repository.Update(map);
        }
    }
}
