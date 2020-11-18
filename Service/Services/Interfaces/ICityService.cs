using Service.DTO;
using System;
using System.Collections.Generic;

namespace Service.Services.Interfaces
{
    public interface ICityService
    {
        IEnumerable<CityGetDTO> GetCities();
        CityGetDTO GetCity(Guid id);
        CityCreateDTO CreateCity(CityCreateDTO city);
        CityCreateDTO UpdateCity(Guid id, CityCreateDTO city);
        bool DeleteCity(Guid id);
    }
}