using DesktopApp.APIInteraction.Mapper;
using DesktopApp.Models;
using Service.PathResolver;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public class TravelSalesmanService : ITravelSalesmanService
    {
        public async Task<HttpResponsePayload<TravelSalesman>> PostCities(TravelSalesmanRequest request, int selectedMethodIndex)
        {
            HttpResponseMessage response = default;
            switch (selectedMethodIndex)
            {
                case 0: response = await APIClient.Client.PostAsJsonAsync("pathresolver/solve-travel-salesman-annealing", request); break;
                case 1: response = await APIClient.Client.PostAsJsonAsync("pathresolver/solve-travel-salesman-nearest", request); break;
                case 2: response = await APIClient.Client.PostAsJsonAsync("pathresolver/solve-travel-salesman-quickest", request); break;
            }
            HttpResponsePayload<TravelSalesman> payload = new HttpResponsePayload<TravelSalesman>();
            payload.IsSuccessful = response.IsSuccessStatusCode;
            if (payload.IsSuccessful)
            {
                var returnedContent = await response.Content.ReadAsAsync<TravelSalesmanResponse>();
                payload.Payload = AppMapper.GetAppMapper().Mapper.Map<TravelSalesmanResponse, TravelSalesman>(returnedContent);
            }
            return payload;
        }
    }
}
