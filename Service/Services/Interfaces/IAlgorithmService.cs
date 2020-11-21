using Service.DTO;
using Service.PathResolver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAlgorithmService
    {
        ShortestPathResponseDTO FindShortestPath(Guid MapId, Guid CityToId, Guid CityFromId);

        Task<TravelSalesmanResponse> SolveAnnealingTravelSalesman(TravelSalesmanRequest requestBody);

        Task<TravelSalesmanResponse> SolveNearestNeghborTravelSalesman(TravelSalesmanRequest requestBody);
    }
}
