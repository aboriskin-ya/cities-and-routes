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
            catch (HttpRequestException ex)
            {
                throw ex;
            }

            HttpResponsePayload<Route> responsePayload = new HttpResponsePayload<Route>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };
            var routeGetDTO = await response.Content.ReadAsAsync<RouteGetDTO>();
            responsePayload.Payload = AppMapper.GetAppMapper().Mapper.Map<Route>(routeGetDTO);

            return responsePayload;
        }
    }
}