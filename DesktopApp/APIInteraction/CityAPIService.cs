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
        public async Task<HttpResponsePayload<City>> CreateCityAsync(City city)
        {
            var cityDTO = AppMapper.GetAppMapper().Mapper.Map<CityCreateDTO>(city);

            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.PostAsJsonAsync("city", cityDTO);
            }
            catch
            {
                return new HttpResponsePayload<City>() { IsSuccessful = false };
            }

            HttpResponsePayload<City> responsePayload = new HttpResponsePayload<City>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };
            var cityGetDTO = await response.Content.ReadAsAsync<CityGetDTO>();
            responsePayload.Payload = AppMapper.GetAppMapper().Mapper.Map<City>(cityGetDTO);

            return responsePayload;
        }

        public async Task<HttpResponsePayload<City>> UpdateCityAsync(City city)
        {
            var cityDTO = AppMapper.GetAppMapper().Mapper.Map<CityCreateDTO>(city);

            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.PutAsJsonAsync("city/" + city.Id, cityDTO);
            }
            catch
            {
                return new HttpResponsePayload<City>() { IsSuccessful = false };
            }

            HttpResponsePayload<City> responsePayload = new HttpResponsePayload<City>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };
            var cityGetDTO = await response.Content.ReadAsAsync<CityGetDTO>();
            responsePayload.Payload = AppMapper.GetAppMapper().Mapper.Map<City>(cityGetDTO);

            return responsePayload;
        }

        public async Task<HttpResponsePayload<City>> DeleteCityAsync(City city)
        {
            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.DeleteAsync("city/" + city.Id);
            }
            catch
            {
                return new HttpResponsePayload<City>() { IsSuccessful = false };
            }

            HttpResponsePayload<City> responsePayload = new HttpResponsePayload<City>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };

            return responsePayload;
        }

        public async Task<HttpResponsePayload<City>> GetCityAsync(Guid guid)
        {
            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.GetAsync($"city/{guid}");
            }
            catch
            {
                return new HttpResponsePayload<City>() { IsSuccessful = false };
            }

            var responsePayload = new HttpResponsePayload<City>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };
            var cityGetDTO = await response.Content.ReadAsAsync<CityGetDTO>();
            responsePayload.Payload = AppMapper.GetAppMapper().Mapper.Map<City>(cityGetDTO);

            return responsePayload;
        }
    }
}