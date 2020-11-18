using Service.DTO;
using System;
using System.Collections.Generic;

namespace Service.Services.Interfaces
{
    public interface ICityService
    {
        IEnumerable<CityGetDTO> GetCities();
        CityGetDTO GetCity(Guid id);
        CityGetDTO CreateCity(CityCreateDTO city);
        CityGetDTO UpdateCity(Guid id, CityCreateDTO city);
        bool DeleteCity(Guid id);
    }
}