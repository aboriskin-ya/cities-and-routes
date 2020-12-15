using DesktopApp.Models;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public interface ITravelSalesmanService
    {
        Task<HttpResponsePayload<TravelSalesman>> Resolve(TravelSalesmanModel request, int selectedMethodIndex);
    }
}
