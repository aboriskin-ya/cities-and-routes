using AutoMapper;
using DesktopApp.APIInteraction.Mapper;
using DesktopApp.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public class CityAPIService : ICityAPIService
    {
        public async Task<Uri> CreateCityAsync(City city)
        {
            var cityDTO =  MyMapper.GetMapper().Map<DataAccess.Models.CityDTO>(city);

            HttpResponseMessage response = await APIClient.Client.PostAsJsonAsync("city", cityDTO);
            response.EnsureSuccessStatusCode();
            
            return response.Headers.Location;
        }
    }
}
