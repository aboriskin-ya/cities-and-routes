using Service.PathResolver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAlgorithmService
    {
        List<Guid> FindShortestPath(Guid MapId, Guid CityToId, Guid CityFromId);

        Task<TravelSalesmanResponse> SolveAnnealingTravelSalesman(TravelSalesmanRequest requestBody);

        Task<TravelSalesmanResponse> SolveNearestNeghborTravelSalesman(TravelSalesmanRequest requestBody);
    }
}
