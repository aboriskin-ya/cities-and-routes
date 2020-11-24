using DesktopApp.Models;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    interface IPathResolverAPIService
    {
        Task<HttpResponsePayload<ShortestPath>> FindShortestPathAsync(PathModel path);
    }
}