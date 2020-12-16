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
        ShortestPathResponseDTO result;
        private readonly ILogger<ShortestPathResolverService> _logger;

        public ShortestPathResolverService(ILogger<ShortestPathResolverService> logger)
        {
            _logger = logger;
            result = new ShortestPathResponseDTO();
            result.FinalDistance = 0;
        }

        public ShortestPathResolverService()
        {
            _logger = new Logger<ShortestPathResolverService>(new LoggerFactory());
            result = new ShortestPathResponseDTO();
            result.FinalDistance = 0;
        }

        public ShortestPathResponseDTO FindShortestPath(ShortPathResolverDTO PathResolverDTO, string startName, string finishName)
        {
            _logger.LogInformation("Find shortest path first function started");
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

        public ShortestPathResponseDTO FindShortestPath(Graph graph, string startName, string finishName)
        {
            _logger.LogInformation("Find shortest path second function started");
            Graph = new Graph();
            foreach (var vertex in graph.Vertices)
            {
                Graph.AddVertex(vertex.Name);
            }
            foreach (var edge in graph.Edges)
            {
                Graph.AddEdge(edge.FirstVertex.Name, edge.SecondVertex.Name, edge.EdgeWeight);
            }
            foreach (var vertex in Graph.Vertices)
            {
                vertex.EdgesWeightSum = int.MaxValue;
                vertex.IsUnvisited = true;
                vertex.NextVertices = new List<GraphVertex>();
                vertex.PreviousVertex = null;
            }
            return FindShortestPath(Graph.FindVertex(startName), Graph.FindVertex(finishName));
        }

        public ShortestPathResponseDTO FindShortestPath(GraphVertex startVertex, GraphVertex finishVertex)
        {
            _logger.LogInformation("Find shortest path third function started");
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

        ShortestPathResponseDTO GetPath(GraphVertex startVertex, GraphVertex endVertex)
        {
            List<Guid> ResultList = new List<Guid>();
            result.FinalDistance = endVertex.EdgesWeightSum;
            while (startVertex != endVertex)
            {
                if (endVertex.PreviousVertex == null)
                {
                    return null;
                }
                ResultList.Add(Guid.Parse(endVertex.ToString()));
                endVertex = endVertex.PreviousVertex;
            }
            ResultList.Add(Guid.Parse(startVertex.ToString()));
            ResultList.Reverse();
            result.Path = ResultList;
            foreach (var vertex in Graph.Vertices)
            {
                vertex.EdgesWeightSum = int.MaxValue;
                vertex.IsUnvisited = true;
                vertex.NextVertices = new List<GraphVertex>();
                vertex.PreviousVertex = null;
            }
            return result;
        }
    }
}
