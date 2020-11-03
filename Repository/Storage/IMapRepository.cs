using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Repository.Storage
{
    public interface IMapRepository : IRepository<Map> 
    {
        Map GetWholeMap(Guid id);
    }
}
