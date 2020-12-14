using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Storage
{
    public interface IMapRepository : IRepository<Map>
    {
        Map GetWholeMap(Guid Id);
        Task<IEnumerable<MapInfo>> GetMapInfoAsync();
    }
}
