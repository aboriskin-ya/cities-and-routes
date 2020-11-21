using DesktopApp.APIInteraction.Mapper;
using DesktopApp.Models;
using Service.DTO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public class RouteAPIService : IRouteAPIService
    {
        public async Task<HttpResponsePayload<Route>> CreateRouteAsync(Route route)
        {
            var routeDTO = AppMapper.GetAppMapper().Mapper.Map<RouteCreateDTO>(route);

            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.PostAsJsonAsync("route", routeDTO);
            }
            catch
            {
                return new HttpResponsePayload<Route>() { IsSuccessful = false };
            }

            HttpResponsePayload<Route> responsePayload = new HttpResponsePayload<Route>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };
            var routeGetDTO = await response.Content.ReadAsAsync<RouteGetDTO>();
            responsePayload.Payload = AppMapper.GetAppMapper().Mapper.Map<Route>(routeGetDTO);

            return responsePayload;
        }

        public async Task<HttpResponsePayload<Route>> UpdateRouteAsync(Route Route)
        {
            var RouteDTO = AppMapper.GetAppMapper().Mapper.Map<RouteCreateDTO>(Route);

            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.PutAsJsonAsync("route/" + Route.Id, RouteDTO);
            }
            catch
            {
                return new HttpResponsePayload<Route>() { IsSuccessful = false };
            }

            HttpResponsePayload<Route> responsePayload = new HttpResponsePayload<Route>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };
            var RouteGetDTO = await response.Content.ReadAsAsync<RouteGetDTO>();
            responsePayload.Payload = AppMapper.GetAppMapper().Mapper.Map<Route>(RouteGetDTO);

            return responsePayload;
        }


        public async Task<HttpResponsePayload<Route>> DeleteRouteAsync(Route Route)
        {

            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.DeleteAsync("route/" + Route.Id);
            }
            catch
            {
                return new HttpResponsePayload<Route>() { IsSuccessful = false };
            }

            HttpResponsePayload<Route> responsePayload = new HttpResponsePayload<Route>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };

            return responsePayload;
        }
    }
}