using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface ICityService
    {
        IEnumerable<City> GetCity();
        City GetCity(Guid id);
        void CreateCity(City city);
        City UpdateCity(City city);
        bool DeleteCity(Guid id);
    }
}
