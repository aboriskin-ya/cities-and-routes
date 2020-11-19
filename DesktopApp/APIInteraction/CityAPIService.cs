using DesktopApp.APIInteraction.Mapper;
using DesktopApp.Models;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            catch (HttpRequestException ex)
            {
                throw ex;
            }

            HttpResponsePayload<City> responsePayload = new HttpResponsePayload<City>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };
            var cityGetDTO = await response.Content.ReadAsAsync<CityGetDTO>();
            responsePayload.Payload = AppMapper.GetAppMapper().Mapper.Map<City>(cityGetDTO);

            return responsePayload;
        }
        public async Task<HttpResponsePayload<CityGetDTO>> GetCity(Guid id)
        {
            var response = await APIClient.Client.GetAsync($"city/{id}");
            HttpResponsePayload<CityGetDTO> payload = new HttpResponsePayload<CityGetDTO>();
            payload.IsSuccessful = response.IsSuccessStatusCode;
            if (payload.IsSuccessful)
            {
                var city = await response.Content.ReadAsAsync<City>();
                payload.Payload = AppMapper.GetAppMapper().Mapper.Map<City, CityGetDTO>(city);
            }
            return payload;
            
        }
    }
}