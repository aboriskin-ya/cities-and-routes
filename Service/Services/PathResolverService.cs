using DataAccess.Models;
using Repository.Storage;
using Repository;
using System;
using System.Collections.Generic;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class PathResolverService : IPathResolverService
    {
        private readonly IMapRepository _mapRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IPathToGraphService _pathService;
        protected readonly CityRouteContext _context;

        public PathResolverService(IMapRepository MapRepository, ICityRepository CityRepository, IPathToGraphService PathService, CityRouteContext Context)
        {
            _context = Context;
            _mapRepository = MapRepository;
            _cityRepository = CityRepository;
            _pathService = PathService;
        }

        public List<Guid> FindPath(Guid MapId, Guid CityToId, Guid CityFromId)
        {
            List<City> CityList = new List<City>();//= this.GetAllCityByMap(MapId);

            return _pathService.CityListToGraph(CityList, CityFromId, CityToId);
        }

    }
}
