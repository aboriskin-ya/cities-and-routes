using PathResolver;
using Service.PathResolver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Service.Services.Interfaces
{
    public class TravelSalesmanNearestNeighbor : ITravelSalesmanNearestNeighbor
    {
        private double _result = 0;
        private double _minWeightValue = double.MaxValue;
        private bool _allVisited = false;
        private List<Guid> _sequence;
        private GraphVertex _currentVertex;
        private Stopwatch _timeCounter;
        public TravelSalesmanNearestNeighbor()
        {
            _sequence = new List<Guid>();
            _timeCounter = new Stopwatch();
        }
        public TravelSalesmanResponse Solve(Graph graph)
        {
            _timeCounter.Start();
            foreach (var edge in graph.Edges)
            {
                graph.FindVertex(edge.FirstVertex.Name).AddNextVertex(edge.SecondVertex);
                graph.FindVertex(edge.FirstVertex.Name).AddEdge(edge.FirstVertex, edge.SecondVertex, edge.EdgeWeight);
            }
            var vertex = graph.Vertices[0];
            vertex.IsUnvisited = false;
            while (!_allVisited)
            {
                _sequence.Add(Guid.Parse(vertex.Name));
                foreach (var nextVertice in vertex.NextVertices)
                {
                    if (nextVertice.IsUnvisited)
                    {
                        var edge = vertex.GetEdge(vertex, nextVertice);
                        if (edge.EdgeWeight < _minWeightValue)
                        {
                            _minWeightValue = edge.EdgeWeight;
                            _currentVertex = nextVertice;
                        }
                    }
                }
                if (vertex.NextVertices.Count == 0 || _minWeightValue == int.MaxValue)
                {
                    foreach (var nextVertice in graph.Vertices)
                    {
                        if (nextVertice.IsUnvisited)
                        {
                            var edgeWeight = new ShortestPathResolverService().
                        FindShortestPath(graph, vertex.Name, nextVertice.Name).FinalDistance;
                            if (edgeWeight < _minWeightValue && vertex.Name != nextVertice.Name)
                            {
                                _minWeightValue = edgeWeight;
                                _currentVertex = nextVertice;
                            }
                        }
                    }
                }              
                _currentVertex.IsUnvisited = false;
                vertex = _currentVertex;
                if (_sequence.Count != graph.Vertices.Count && _minWeightValue != int.MaxValue)
                    _result += _minWeightValue;
                _minWeightValue = int.MaxValue;
                if (_sequence.Count == graph.Vertices.Count && _currentVertex.IsUnvisited == false)
                {
                    _result += new ShortestPathResolverService().
                        FindShortestPath(graph, _sequence.Last().ToString(), _sequence.First().ToString()).FinalDistance;
                    _allVisited = true;
                }
            }
            _timeCounter.Stop();
            var response = new TravelSalesmanResponse()
            {
                PreferableSequenceOfCities = _sequence,
                CalculatedDistance = _result,
                NameAlghorithm = nameof(TravelSalesmanNearestNeighbor),
                ProcessDuration = GetProcessDuration(_timeCounter.Elapsed)
            };
            return response;
        }
        private string GetProcessDuration(TimeSpan timeSpan)
        {
            var seconds = timeSpan.Seconds.ToString();
            var milliSeconds = timeSpan.TotalMilliseconds;
            return $"{seconds}s,{milliSeconds}ms.";
        }
    }
}