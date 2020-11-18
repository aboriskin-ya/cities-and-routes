using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public class TravelSalesmanService
    {
        public async Task<IEnumerable<Guid>> PostSelectedCitiesAsync(IEnumerable<Guid> IdCollection)
        {
            var response = await APIClient.Client.PostAsJsonAsync("pathresolver/solve-travel-salesman-annealing", IdCollection);
            IEnumerable<Guid> resultColection = default;
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                resultColection = content.Split(',').Select(Guid.Parse);
            }
            return IdCollection;
        }
    }
}
