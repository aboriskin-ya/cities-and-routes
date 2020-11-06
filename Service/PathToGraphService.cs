using DataAccess.Models;
using System;
using System.Collections.Generic;
using DataAccess.DTO;
using PathResolver;

namespace Service
{
    public class PathToGraphService : IPathToGraphService
    {
        public List<Guid> CityListToGraph(IEnumerable<City> CityList, Guid CityFromId, Guid CityToId)
        {
            var Graph = new Graph();

            foreach (City City in CityList)
            {
                Graph.AddVertex(City.Id.ToString());
            }

            foreach (City City in CityList)
            {
                foreach (City OtherCity in CityList)
                {
                    if (City.Id != OtherCity.Id && City.Id != CityToId && City.Id != CityFromId)
                    {
                       Graph.AddEdge(City.Id.ToString(), OtherCity.Id.ToString(), 123);
                    }
                }
            }

            var GraphService = new GraphService(Graph);
            var Path = GraphService.FindShortestPath(CityFromId.ToString(), CityToId.ToString());

            return Path;
        }

    }
}
