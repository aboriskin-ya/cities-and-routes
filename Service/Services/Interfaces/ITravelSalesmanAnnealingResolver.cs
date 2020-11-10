using System;
using System.Collections.Generic;
using DataAccess.DTO;
using PathResolver;
using Service.DTO;

namespace Service
{
    public interface ITravelSalesmanAnnealingResolver
    {
        IEnumerable<Guid> Resolve(Graph Graph);
    }
}
