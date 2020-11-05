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
            var cityDTO =  AppMapper.GetAppMapper().Mapper.Map<DataAccess.Models.CityDTO>(city);

            HttpResponseMessage response = await APIClient.Client.PostAsJsonAsync("city", cityDTO);
            
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"We got a http status code: {response.StatusCode}");
        
            return response.Headers.Location;
        }
    }
}
