using DataAccess.DTO;
using DesktopApp.APIInteraction.Mapper;
using DesktopApp.Models;
using Service.DTO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    class PathResolverAPIService : IPathResolverAPIService
    {
        public async Task<HttpResponsePayload<ShortestPath>> FindShortestPathAsync(PathModel path)
        {
            var pathDTO = AppMapper.GetAppMapper().Mapper.Map<PathResolverDTO>(path);

            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.PostAsJsonAsync("pathresolver/FindShortestPath", pathDTO);
            }
            catch
            {
                return new HttpResponsePayload<ShortestPath>() { IsSuccessful = false };
            }

            var responsePayload = new HttpResponsePayload<ShortestPath>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };
            var shortestPathDTO = await response.Content.ReadAsAsync<ShortestPathResponseDTO>();
            responsePayload.Payload = AppMapper.GetAppMapper().Mapper.Map<ShortestPath>(shortestPathDTO);

            return responsePayload;
        }
    }
}