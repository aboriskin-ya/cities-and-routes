using DataAccess.Models;
using Repository.Storage;
using System;
using System.Collections.Generic;

namespace Repository.Storage
{
    public interface ICityRepository : IRepository<DataAccess.Models.City>
    {
        List<City> GetAllCityByMap(Guid mapId);
    }

}