using DesktopApp.Models;
using Service.PathResolver;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public interface ITravelSalesmanService
    {
        Task<HttpResponsePayload<TravelSalesman>> PostCities(TravelSalesmanRequest request, int selectedMethodIndex);
    }
}
