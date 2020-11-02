using DesktopApp.Models;
using System;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public interface ICityAPIService
    {
        Task<Uri> CreateCityAsync(City city);
    }
}
