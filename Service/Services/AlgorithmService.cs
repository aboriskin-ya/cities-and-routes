using DataAccess.Models;
using Repository.Storage;
using Repository;
using System;
using System.Collections.Generic;
using Service.Services.Interfaces;
using Service.DTO;

namespace Service.Services
{
    public class AlgorithmService : Interfaces.IAlgorithmService
    {
        private readonly IMapRepository _mapRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IPathToGraphService _pathToGraphService;
        protected readonly CityRouteContext _context;

        public AlgorithmService(IMapRepository MapRepository, ICityRepository CityRepository, IPathToGraphService PathService, CityRouteContext Context)
        {
            _context = Context;
            _mapRepository = MapRepository;
            _cityRepository = CityRepository;
            _pathToGraphService = PathService;
        }
        
        public List<Guid> FindShortestPath(Guid MapId, Guid CityToId, Guid CityFromId)
        {
            Map Map = _mapRepository.GetWholeMap(MapId);
            ShortPathResolverDTO PathDto = _pathToGraphService.MapToGraph(Map);
            List<Guid>Path = new ShortestPathResolverService().FindShortestPath(PathDto, CityFromId.ToString(), CityToId.ToString());

            return Path;
        }

    }
}
