using DesktopApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public interface IMapAPIService
    {
        Task<HttpResponsePayload<Map>> CreateMapAsync(Map map);

        Task<HttpResponsePayload<List<Map>>> GetAllNamesMapAsync();

        Task<bool> DeleteMapAsync(Guid guid);

        Task<HttpResponsePayload<WholeMap>> GetMapAsync(Guid guid);
    }
}