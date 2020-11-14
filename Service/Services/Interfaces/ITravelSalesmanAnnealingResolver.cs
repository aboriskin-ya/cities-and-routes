using PathResolver;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface ITravelSalesmanAnnealingResolver
    {
        IEnumerable<Guid> Resolve(Graph Graph);
    }
}
