using DesktopApp.Models;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public interface ICityAPIService
    {
        Task<HttpResponsePayload<City>> CreateCityAsync(City city);
    }
}
