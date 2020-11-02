using DataAccess.Models;
using Service.DTO;
using System;
using System.Collections.Generic;

namespace Service.Services.Interfaces
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
