using DataAccess.Models;
using System;

namespace Repository.Storage
{
    public interface IMapRepository : IRepository<Map>
    {
        Map GetWholeMap(Guid Id);
    }
}
