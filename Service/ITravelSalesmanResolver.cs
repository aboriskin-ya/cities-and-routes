using Service.Models;
using System.Collections.Generic;

namespace Service
{
    public interface ITravelSalesmanResolver
    {
        IEnumerable<int> Resolve(IEnumerable<int> Vertexes, GraphDTO Graph);
    }
}
