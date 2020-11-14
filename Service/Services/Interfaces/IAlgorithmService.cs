using Service.PathResolver;
using System;
using System.Collections.Generic;

namespace Service.Services.Interfaces
{
    public interface IAlgorithmService
    {
        List<Guid> FindShortestPath(Guid MapId, Guid CityToId, Guid CityFromId);

        IEnumerable<Guid> SolveTravelSalesman(TravelSalesmanRequest requestBody);
    }
}
