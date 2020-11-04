using System.Collections.Generic;
using System.Net.Http;

namespace Service
{
    interface ITravelSalesmanResolver
    {
        IEnumerable<int> CalcAppropriatePath(HttpRequestMessage request);
    }
}
