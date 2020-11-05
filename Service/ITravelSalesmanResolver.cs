using System.Collections.Generic;
using Service.Models;

namespace Service
{
    interface ITravelSalesmanResolver
    {
        IEnumerable<int> Resolve(IEnumerable<int> Vertexes, GraphDTO Graph);
    }
}
