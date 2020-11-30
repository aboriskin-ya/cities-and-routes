using DataAccess.Models;
using Microsoft.Extensions.Logging;
using PathResolver;
using Service.DTO;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Service.Services
{
    public class ShortestPathResolverService : IShortestPathResolverService
    {
        public Graph Graph;
        ShortestPathResponseDTO result;
        Stopwatch _timeCounter;
        private readonly ILogger<ShortestPathResolverService> _logger;

        public ShortestPathResolverService(ILogger<ShortestPathResolverService> logger)
        {
            _logger = logger;
            result = new ShortestPathResponseDTO();
            result.FinalDistance = 0;
            _timeCounter = new Stopwatch();
        }

        public ShortestPathResolverService()
        {
            _logger = new Logger<ShortestPathResolverService>(new LoggerFactory());
            result = new ShortestPathResponseDTO();
            result.FinalDistance = 0;
            _timeCounter = new Stopwatch();
        }

        public ShortestPathResponseDTO FindShortestPath(ShortPathResolverDTO PathResolverDTO, string startName, string finishName)
        {
            _logger.LogInformation("Find shortest path first function started");
            _timeCounter.Start();
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

        public ShortestPathResponseDTO FindShortestPath(Graph Graph, string startName, string finishName)
        {
            _logger.LogInformation("Find shortest path second function started");
            this.Graph = Graph;
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
            _timeCounter.Stop();
            result.ProcessDuration = GetProcessDuration(_timeCounter.Elapsed);
            return result;
        }

        private string GetProcessDuration(TimeSpan timeSpan)
        {
            var seconds = timeSpan.Seconds.ToString();
            var milliSeconds = timeSpan.Milliseconds;
            return $"{seconds}s,{milliSeconds}ms.";
        }
    }
}
