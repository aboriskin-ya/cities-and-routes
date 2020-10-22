using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IMapService
    {
        IEnumerable<Map> GetMap();
        Map GetMap(Guid id);
        void CreateMap(Map map);
        Map UpdateMap(Map map);
        bool DeleteMap(Guid id);
    }
}
