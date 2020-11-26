using DesktopApp.Models;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public interface IRouteAPIService
    {
        Task<HttpResponsePayload<Route>> CreateRouteAsync(Route route);
        Task<HttpResponsePayload<Route>> UpdateRouteAsync(Route city);
        Task<HttpResponsePayload<Route>> DeleteRouteAsync(Route city);
    }
}