using DesktopApp.Models;
using Service.PathResolver;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public interface ITravelSalesmanService
    {
        Task<HttpResponsePayload<TravelSalesman>> Resolve(TravelSalesmanRequest request, int selectedMethodIndex);
    }
}
