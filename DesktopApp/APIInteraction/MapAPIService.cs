using DesktopApp.APIInteraction.Mapper;
using DesktopApp.Models;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public class MapAPIService : IMapAPIService
    {
        public async Task<HttpResponsePayload<Map>> CreateMapAsync(Map map)
        {
            var mapDTO = AppMapper.GetAppMapper().Mapper.Map<MapCreateDTO>(map);

            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.PostAsJsonAsync("map", mapDTO);
            }
            catch
            {
                return new HttpResponsePayload<Map>() { IsSuccessful = false };
            }

            var responsePayload = new HttpResponsePayload<Map>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };
            var mapGetDTO = await response.Content.ReadAsAsync<MapGetDTO>();
            responsePayload.Payload = AppMapper.GetAppMapper().Mapper.Map<Map>(mapGetDTO);

            return responsePayload;
        }

        public async Task<HttpResponsePayload<List<Map>>> GetAllNamesMapAsync()
        {
            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.GetAsync("map/getallnames");
            }
            catch
            {
                return new HttpResponsePayload<List<Map>>() { IsSuccessful = false };
            }

            var responsePayload = new HttpResponsePayload<List<Map>>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };
            var listMapGetDTO = await response.Content.ReadAsAsync<List<MapIdNameGetDTO>>();
            responsePayload.Payload = AppMapper.GetAppMapper().Mapper.Map<List<Map>>(listMapGetDTO);

            return responsePayload;
        }

        public async Task<bool> DeleteMapAsync(Guid guid)
        {
            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.DeleteAsync($"map/{guid}");
            }
            catch
            {
                return false;
            }

            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<HttpResponsePayload<WholeMap>> GetMapAsync(Guid guid)
        {
            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.GetAsync($"map/{guid}");
            }
            catch
            {
                return new HttpResponsePayload<WholeMap>() { IsSuccessful = false };
            }

            var responsePayload = new HttpResponsePayload<WholeMap>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };
            var mapGetDTO = await response.Content.ReadAsAsync<MapGetDTO>();
            responsePayload.Payload = AppMapper.GetAppMapper().Mapper.Map<WholeMap>(mapGetDTO);

            return responsePayload;
        }
    }
}