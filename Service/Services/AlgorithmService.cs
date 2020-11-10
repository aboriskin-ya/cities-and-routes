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
using Repository;
using Repository.Storage;
using Service.DTO;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Service.Services
{
    public class AlgorithmService : Interfaces.IAlgorithmService
    {
        private readonly ITravelSalesmanAnnealingResolver _resolver;
        private readonly IMapRepository _mapRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IPathToGraphService _pathToGraphService;
        protected readonly CityRouteContext _context;

        public AlgorithmService(IMapRepository MapRepository,
                                ICityRepository CityRepository,
                                IPathToGraphService PathService,
                                CityRouteContext Context,
                                ITravelSalesmanAnnealingResolver resolver)
        {
            _context = Context;
            _mapRepository = MapRepository;
            _cityRepository = CityRepository;
            _pathToGraphService = PathService;
            _resolver = resolver;
        }

        public List<Guid> FindShortestPath(Guid MapId, Guid CityToId, Guid CityFromId)
        {
            Map Map = _mapRepository.GetWholeMap(MapId);
            ShortPathResolverDTO PathDto = _pathToGraphService.MapToResolver(Map);
            List<Guid> Path = new ShortestPathResolverService().FindShortestPath(PathDto, CityFromId.ToString(), CityToId.ToString());
            return Path;
        }
        public IEnumerable<Guid> SolveTravelSalesman(TravelSalesmanRequest request)
        {
            Map map = _mapRepository.GetWholeMap(request.MapId);
            if (request.SelectedCities.Count() > 0 && map != null)
            {
                Graph graph = _pathToGraphService.MapToGraph(map,request.SelectedCities);
                return _resolver.Resolve(graph);
            }
            return default;
        }
    }
}
