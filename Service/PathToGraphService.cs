using DataAccess.Models;
using System;
using System.Collections.Generic;
using DataAccess.DTO;
using PathResolver;

namespace Service
{
    public class PathToGraphService : IPathToGraphService
    {
        public List<Guid> CityListToGraph(IEnumerable<City> cityList, Guid cityFromId, Guid cityToId)
        {
            var graph = new Graph();

            foreach (City city in cityList)
            {
                graph.AddVertex(city.Id.ToString());
            }

            foreach (City city in cityList)
            {
                foreach (City otherCity in cityList)
                {
                    if (city.Id != otherCity.Id && city.Id != cityToId && city.Id != cityFromId)
                    {
                       graph.AddEdge(city.Id.ToString(), otherCity.Id.ToString(), this.calculateDistance(city, otherCity));
                    }
                }
            }

            var graphService = new GraphService(graph);
            var path = graphService.FindShortestPath(cityFromId.ToString(), cityToId.ToString());

            return path;
        }

        public int calculateDistance(City city, City otherCity)
        {
            int distance;

            distance = city.X - otherCity.X + city.Y - otherCity.Y;
            if (distance < 0)
            {
                distance *= (-1);
            }
            return distance;
        }
    }
}
