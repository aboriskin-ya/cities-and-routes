using AutoMapper;
using DesktopApp.APIInteraction.Mapper;
using DesktopApp.Models;
using Service.DTO;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public class CityAPIService : ICityAPIService
    {
        public async Task<Uri> CreateCityAsync(City city)
        {
            var cityDTO =  AppMapper.GetAppMapper().Mapper.Map<CityDTO>(city);

            HttpResponseMessage response = await APIClient.Client.PostAsJsonAsync("city", cityDTO);
            response.EnsureSuccessStatusCode();
            
            return response.Headers.Location;
        }
    }
}
