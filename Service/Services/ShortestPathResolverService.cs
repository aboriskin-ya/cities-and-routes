using DataAccess.Models;
using Microsoft.Extensions.Logging;
using PathResolver;
using Service.DTO;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Service.Services
{
    public class ShortestPathResolverService : IShortestPathResolverService
    {
        public Graph Graph;
        private readonly ILogger<ShortestPathResolverService> _logger;

        public ShortestPathResolverService(ILogger<ShortestPathResolverService> logger)
        {
            _logger = logger;
        }

        public List<Guid> FindShortestPath(ShortPathResolverDTO PathResolverDTO, string startName, string finishName)
        {
            _logger.LogInformation("Find shortest path started");
            Graph = new Graph();
            foreach (City City in PathResolverDTO.Cities)
            {
                Graph.AddVertex(City.Id.ToString());
            }

            foreach (Route Route in PathResolverDTO.Routes)
            {
                Graph.AddEdge(Route.FirstCityId.ToString(), Route.SecondCityId.ToString(), Route.Distance);
            }

            return FindShortestPath(Graph.FindVertex(startName), Graph.FindVertex(finishName));
        }

        public List<Guid> FindShortestPath(Graph Graph, string startName, string finishName)
        {
            _logger.LogInformation("Find shortest path started");
            this.Graph = Graph;
            return FindShortestPath(Graph.FindVertex(startName), Graph.FindVertex(finishName));
        }

        public List<Guid> FindShortestPath(GraphVertex startVertex, GraphVertex finishVertex)
        {
            _logger.LogInformation("Find shortest path started");
            startVertex.EdgesWeightSum = 0;
            while (true)
            {
                var current = FindUnvisitedVertexWithMinSum();
                if (current == null)
                {
                    break;
                }

                SetSumToNextVertex(current);
            }

            return GetPath(startVertex, finishVertex);
        }

        public GraphVertex FindUnvisitedVertexWithMinSum()
        {
            var minValue = int.MaxValue;
            GraphVertex minVertexInfo = null;
            foreach (var Vertex in Graph.Vertices)
            {
                if (Vertex.IsUnvisited && Vertex.EdgesWeightSum < minValue)
                {
                    minVertexInfo = Vertex;
                    minValue = Vertex.EdgesWeightSum;
                }
            }

            return minVertexInfo;
        }
        void SetSumToNextVertex(GraphVertex info)
        {
            info.IsUnvisited = false;
            foreach (var Edge in info.Edges)
            {
                var nextInfo = Edge.ConnectedVertex;
                var sum = info.EdgesWeightSum + Edge.EdgeWeight;
                if (sum < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = sum;
                    nextInfo.PreviousVertex = info;
                }
            }
        }

        List<Guid> GetPath(GraphVertex startVertex, GraphVertex endVertex)
        {
            var path = endVertex.ToString();
            List<Guid> ResultList = new List<Guid>();
            while (startVertex != endVertex)
            {
                endVertex = endVertex.PreviousVertex;
                ResultList.Add(Guid.Parse(endVertex.ToString()));
            }

            return ResultList;
        }
    }
}
