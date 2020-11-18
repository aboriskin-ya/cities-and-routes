using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public interface ITravelSalesmanService
    {
        Task<IEnumerable<Guid>> PostSelectedCitiesAsync(IEnumerable<Guid> IdCollection);
    }
}
