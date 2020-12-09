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
                var vertex = graph.Vertices[i];
                for (int j = 0; j < citiesArr.Count(); j++)
                {
                    if (i == j) continue;
                    var route = map.Routes.FirstOrDefault(t => t.FirstCityId == citiesArr[i] && t.SecondCityId == citiesArr[j]);
                    if (route == null)
                        route = SearchingRoute(map, citiesArr[i], citiesArr[j]);
                    if (route == null) return null;
                        graph.AddEdge(route.FirstCityId.ToString(), route.SecondCityId.ToString(), route.Distance);
                    if (vertex.Name.Equals(route.FirstCityId.ToString()))
                    {
                        var secondVertex = graph.FindVertex(route.SecondCityId.ToString());
                        vertex.AddNextVertex(secondVertex);
                        secondVertex.AddNextVertex(vertex);
                        vertex.AddEdge(vertex, secondVertex, route.Distance);
                        secondVertex.AddEdge(secondVertex, vertex, route.Distance);
                    }
                }
            }
            _logger.LogInformation("Map to graph finished");
            return graph;
        }

        private Route SearchingRoute(Map map, Guid firstCity, Guid secondCity)
        {
            var result = new ShortestPathResolverService()
                .FindShortestPath(MapToResolver(map), firstCity.ToString(), secondCity.ToString());
            if (result == null)
                return null;
            var route = new Route
            {
                Id = new Guid(),
                MapId = map.Id,
                FirstCityId = firstCity,
                SecondCityId = secondCity,
                Distance = result.FinalDistance
            };
            return route;
        }
    }
}
