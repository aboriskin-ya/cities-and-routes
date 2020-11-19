using DesktopApp.Models;
using Service.PathResolver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public interface ITravelSalesmanService
    {
        Task<HttpResponsePayload<TravelSalesman>> PostCities(IEnumerable<Guid> idCollection, int selectedMethodIndex);
    }
}
