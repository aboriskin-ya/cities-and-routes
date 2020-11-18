using DesktopApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public interface ICityAPIService
    {
        Task<HttpResponsePayload<City>> CreateCityAsync(City city);
        Task<IEnumerable<Guid>> GetIdCollection(IEnumerable<string> cityNames);
        Task<IEnumerable<Guid>> PostSelectedCitiesAsync(IEnumerable<Guid> IdCollection);
    }
}