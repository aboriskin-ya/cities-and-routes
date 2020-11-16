using DataAccess.Models;
using PathResolver;
using Service.DTO;
using System;
using System.Collections.Generic;

namespace Service.Services.Interfaces
{
    public interface IPathToGraphService
    {
        ShortPathResolverDTO MapToResolver(Map Map);
        Graph MapToGraph(Map map, IEnumerable<Guid> SelectedCities);
    }
}
