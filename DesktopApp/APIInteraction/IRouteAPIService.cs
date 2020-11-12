using DesktopApp.Models;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public interface IRouteAPIService
    {
        Task<HttpResponsePayload<Route>> CreateRouteAsync(Route route);
    }
}