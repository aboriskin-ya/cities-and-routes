using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface ICityService
    {
        IEnumerable<City> GetCity();
        City GetCity(Guid id);
        City CreateCity(CityDTO city);
        City UpdateCity(Guid id, CityDTO city);
        bool DeleteCity(Guid id);
    }
}
