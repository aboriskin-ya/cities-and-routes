using System.Collections.Generic;
using Service.Models;

namespace Service
{
   public interface ITravelSalesmanResolver
    {
        IEnumerable<int> Resolve(IEnumerable<int> Vertexes, GraphDTO Graph);
    }
}
