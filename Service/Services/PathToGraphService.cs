using AutoMapper;
using DataAccess.Models;
using Service.DTO;
using AutoMapper;
using PathResolver;
using System.Collections.Generic;
using System;
using System.Linq;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class PathToGraphService : IPathToGraphService
    {
        private readonly IMapper _mapper;

        public PathToGraphService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public ShortPathResolverDTO MapToResolver(Map Map)
        {
            ShortPathResolverDTO PathResolverDTO = _mapper.Map<ShortPathResolverDTO>(Map);
            return PathResolverDTO;
        }
        public Graph MapToGraph(Map map, IEnumerable<Guid> SelectedCities)
        {
            var citiesArr = SelectedCities.ToArray();
            var graph = new Graph();
            foreach(var item in SelectedCities)
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
            return graph;
        }

    }
}
