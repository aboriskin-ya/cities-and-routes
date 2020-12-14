using DesktopApp.APIInteraction.Mapper;
using DesktopApp.Models;
using Service.PathResolver;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public class TravelSalesmanService : ITravelSalesmanService
    {
        public async Task<HttpResponsePayload<TravelSalesman>> Resolve(TravelSalesmanModel request, int selectedMethodIndex)
        {
            var travelSalesmanRequest = AppMapper.GetAppMapper().Mapper.Map<TravelSalesmanRequest>(request);

            HttpResponseMessage response = default;
            switch (selectedMethodIndex)
            {
                case 0: response = await APIClient.Client.PostAsJsonAsync("api/pathresolver/solve-travel-salesman-annealing", travelSalesmanRequest); break;
                case 1: response = await APIClient.Client.PostAsJsonAsync("api/pathresolver/solve-travel-salesman-nearest", travelSalesmanRequest); break;
                case 2: response = await APIClient.Client.PostAsJsonAsync("api/pathresolver/solve-travel-salesman-quickest", travelSalesmanRequest); break;
            }
            HttpResponsePayload<TravelSalesman> payload = new HttpResponsePayload<TravelSalesman>();
            payload.IsSuccessful = response.IsSuccessStatusCode;
            if (payload.IsSuccessful)
            {
                var returnedContent = await response.Content.ReadAsAsync<TravelSalesmanResponse>();
                payload.Payload = AppMapper.GetAppMapper().Mapper.Map<TravelSalesman>(returnedContent);
            }
            return payload;
        }
    }
}
