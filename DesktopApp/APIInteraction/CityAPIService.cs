﻿using DesktopApp.APIInteraction.Mapper;
using DesktopApp.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public class CityAPIService : ICityAPIService
    {        
        public async Task<HttpResponsePayload<City>> CreateCityAsync(City city)
        {
            var cityDTO =  AppMapper.GetAppMapper().Mapper.Map<DataAccess.Models.CityDTO>(city);

            HttpResponseMessage response; 

            try
            {
                response = await APIClient.Client.PostAsJsonAsync("city", cityDTO);
            }
            catch(HttpRequestException ex)
            {
                throw ex;
            }

            HttpResponsePayload<City> responsePayload = new HttpResponsePayload<City>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };
            cityDTO = HttpContentExtensions.ReadAsAsync(response.Content, typeof(DataAccess.Models.CityDTO)).Result as DataAccess.Models.CityDTO;
            responsePayload.Payload = AppMapper.GetAppMapper().Mapper.Map<City>(cityDTO);

            return responsePayload;
        }
    }
}
