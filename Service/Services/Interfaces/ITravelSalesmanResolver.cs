using System;
using System.Collections.Generic;
using DataAccess.DTO;
using PathResolver;
using Service.DTO;

namespace Service
{
    public interface ITravelSalesmanResolver
    {
        IEnumerable<Guid> Resolve(IEnumerable<Guid> Vertices, ShortPathResolverDTO CitiesRoutes);
    }
}
