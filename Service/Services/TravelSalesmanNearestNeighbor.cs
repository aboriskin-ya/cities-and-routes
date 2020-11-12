using PathResolver;
using Service.PathResolver;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
                _currentVertex.IsUnvisited = false;
                vertex = _currentVertex;
                if (_sequence.Count != graph.Vertices.Count)
                    _result += _minWeightValue;
                _minWeightValue = int.MaxValue;
                if (_sequence.Count == graph.Vertices.Count && _currentVertex.IsUnvisited == false)
                {
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
            var milliSeconds = timeSpan.Milliseconds;
            return $"{seconds}s,{milliSeconds}ms.";
        }
    }
}