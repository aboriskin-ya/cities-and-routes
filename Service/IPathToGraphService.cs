using DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IPathToGraphService
    {
        List<Guid> CityListToGraph(IEnumerable<City> CityList, Guid CityFromId, Guid CityToId);
    }
}
