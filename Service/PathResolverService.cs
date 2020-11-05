using DataAccess.Models;
using Repository.Storage;
using Repository;
using System;
using System.Collections.Generic;
using DataAccess.DTO;
using PathResolver;

namespace Service
{
    public class PathResolverService : IPathResolverService
    {
        private readonly IMapRepository _mapRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IPathToGraphService _pathService;
        protected readonly CityRouteContext _context;

        public PathResolverService(IMapRepository mapRepository, ICityRepository cityRepository, IPathToGraphService pathService, CityRouteContext context)
        {
            _context = context;
            _mapRepository = mapRepository;
            _cityRepository = cityRepository;
            _pathService = pathService;
        }

        public List<Guid> FindPath(Guid mapId, Guid cityToId, Guid cityFromId)
        {
            List<City> cityList = this.GetAllCityByMap(mapId);

            return _pathService.CityListToGraph(cityList, cityFromId, cityToId);
        }

        public List<City> GetAllCityByMap(Guid mapId)
        {
            return _cityRepository.GetAllCityByMap(mapId);
        }

    }
}
