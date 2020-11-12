using Service.DTO;
using DataAccess.Models;
using PathResolver;
using System.Collections.Generic;
using System;

namespace Service.Services.Interfaces
{
    public interface IPathToGraphService
    {
        ShortPathResolverDTO MapToResolver(Map Map);

        Graph MapToGraph(Map map, IEnumerable<Guid> SelectedCities);
    }
}
