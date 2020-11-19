using DesktopApp.Models;
using Service.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public interface ICityAPIService
    {
        Task<HttpResponsePayload<City>> CreateCityAsync(City city);
        Task<HttpResponsePayload<CityGetDTO>> GetCity(Guid id)
    }
}