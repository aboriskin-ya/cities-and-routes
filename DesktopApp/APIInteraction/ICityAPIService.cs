using DesktopApp.Models;
using System;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public interface ICityAPIService
    {
        Task<HttpResponsePayload<City>> CreateCityAsync(City city);
        Task<HttpResponsePayload<City>> UpdateCityAsync(City city);
        Task<HttpResponsePayload<City>> DeleteCityAsync(City city);
        Task<HttpResponsePayload<City>> GetCityAsync(Guid guid);
    }
}