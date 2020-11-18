using DesktopApp.APIInteraction.Mapper;
using DesktopApp.Models;
using Service.DTO;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Guid>> GetIdCollection(IEnumerable<string> cityNames)
        {
            var response = await APIClient.Client.GetAsync("city/getall");
            var cities = await response.Content.ReadAsAsync<IEnumerable<CityGetDTO>>();
            var IdCollection = cities.Where(t => t.Name.Equals(cityNames.Select(t => t))).Select(t => t.Id);
            return IdCollection;
        }
        public async Task<IEnumerable<Guid>> PostSelectedCitiesAsync(IEnumerable<Guid> IdCollection)
        {
            var response = await APIClient.Client.PostAsJsonAsync("pathresolver/solve-travel-salesman-annealing", IdCollection);
            IEnumerable<Guid> resultColection=default;
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                resultColection = content.Split(',').Select(Guid.Parse);
            }
            return IdCollection;
        }
    }
}