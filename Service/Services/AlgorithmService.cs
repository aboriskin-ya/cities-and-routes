using DataAccess.Models;
using PathResolver;
using Repository;
using Repository.Storage;
using Service.DTO;
using Service.PathResolver;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AlgorithmService : Interfaces.IAlgorithmService
    {
        private readonly ITravelSalesmanNearestNeighbor _nearestResolver;
        private readonly ITravelSalesmanAnnealingResolver _annealingResolver;
        private readonly IMapRepository _mapRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IPathToGraphService _pathToGraphService;
        protected readonly CityRouteContext _context;

        public AlgorithmService(IMapRepository MapRepository,
                                ICityRepository CityRepository,
                                IPathToGraphService PathService,
                                CityRouteContext Context,
                                ITravelSalesmanAnnealingResolver Annealingresolver,
                                ITravelSalesmanNearestNeighbor nearestResolver)
        {
            _context = Context;
            _mapRepository = MapRepository;
            _cityRepository = CityRepository;
            _pathToGraphService = PathService;
            _annealingResolver = Annealingresolver;
            _nearestResolver = nearestResolver;
        }

        public List<Guid> FindShortestPath(Guid MapId, Guid CityToId, Guid CityFromId)
        {
            Map Map = _mapRepository.GetWholeMap(MapId);
            ShortPathResolverDTO PathDto = _pathToGraphService.MapToResolver(Map);
            List<Guid> Path = new ShortestPathResolverService().FindShortestPath(PathDto, CityFromId.ToString(), CityToId.ToString());
            return Path;
        }
        public async Task<TravelSalesmanResponse> SolveAnnealingTravelSalesman(TravelSalesmanRequest request)
        {
            Map map = _mapRepository.GetWholeMap(request.MapId);
            if (request.SelectedCities.Count() > 0 && map != null)
            {
                Graph graph = _pathToGraphService.MapToGraph(map, request.SelectedCities);
                return await Task.Run(() => _annealingResolver.Resolve(graph));
            }
            return default;
        }

        public async Task<TravelSalesmanResponse> SolveNearestNeghborTravelSalesman(TravelSalesmanRequest requestBody)
        {
            Map map = _mapRepository.GetWholeMap(requestBody.MapId);
            if (requestBody.SelectedCities.Count() > 0 && map != null)
            {
                Graph graph = _pathToGraphService.MapToGraph(map, requestBody.SelectedCities);
                return await Task.Run(() => _nearestResolver.Solve(graph));
            }
            return default;
        }
    }
}
