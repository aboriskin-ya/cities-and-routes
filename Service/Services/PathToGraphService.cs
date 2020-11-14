using AutoMapper;
using DataAccess.Models;
using Microsoft.Extensions.Logging;
using PathResolver;
using Service.DTO;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class PathToGraphService : IPathToGraphService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PathToGraphService> _logger;

        public PathToGraphService(IMapper mapper, ILogger<PathToGraphService> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }
        public ShortPathResolverDTO MapToResolver(Map Map)
        {
            ShortPathResolverDTO PathResolverDTO = _mapper.Map<ShortPathResolverDTO>(Map);
            return PathResolverDTO;
        }
        public Graph MapToGraph(Map map, IEnumerable<Guid> SelectedCities)
        {
            _logger.LogInformation("Map to graph started");
            var citiesArr = SelectedCities.ToArray();
            var graph = new Graph();
            foreach (var item in SelectedCities)
            {
                graph.AddVertex(item.ToString());
            }
            for (int i = 0; i < citiesArr.Count(); i++)
            {
                for (int j = 0; j < citiesArr.Count(); j++)
                {
                    if (i == j) continue;
                    var route = map.Routes.FirstOrDefault(t => t.FirstCityId == citiesArr[i] && t.SecondCityId == citiesArr[j]);
                    if (route == null) continue;
                    graph.AddEdge(route.FirstCityId.ToString(), route.SecondCityId.ToString(), route.Distance);
                }
            }
            _logger.LogInformation("Map to graph finished");
            return graph;
        }

    }
}
