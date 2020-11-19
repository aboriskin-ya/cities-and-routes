using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Repository.Storage
{
    public interface ICityRepository : IRepository<DataAccess.Models.City>
    {
        public List<Route> GetRoutes(Guid id);
    }
}