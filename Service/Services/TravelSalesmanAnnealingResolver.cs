using Microsoft.EntityFrameworkCore.Internal;
using PathResolver;
using Service.PathResolver;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Service
{
    public class TravelSalesmanAnnealingResolver : ITravelSalesmanAnnealingResolver
    {
        #region fields
        private IEnumerable<string> _preferableSequnce;
        private double _temperature = 100;
        private double _deltaWeight;
        private double _minWeightValue;
        private double _previosWeightValue;
        private double _currentProbability;
        private int _minLimit;
        private int _maxLimit;
        private double _currentWeightValue = 0;
        private string[] _currentSequence;
        private Stopwatch _timeCounter;
        Graph _graph;
        TravelSalesmanResponse response;

        #endregion
        public TravelSalesmanResponse Resolve(Graph graph)
        {
            _graph = graph;
            Initialize(graph);
            if (CheckExecuting(_minWeightValue))
            {
                if (_currentSequence.Length == 1)
                {
                    return null;
                }
                else if (_currentSequence.Length == 2)
                {
                    response = new TravelSalesmanResponse()
                    {
                        PreferableSequenceOfCities = _currentSequence.Select(Guid.Parse),
                        CalculatedDistance = graph.GetEdge(_currentSequence[0], _currentSequence[1]).EdgeWeight * 2,
                        NameAlghorithm = nameof(TravelSalesmanAnnealingResolver),
                        ProcessDuration = GetProcessDuration(_timeCounter.Elapsed)
                    };
                    return response;
                }
                else if (_currentSequence.Length == 3)
                {
                    response = new TravelSalesmanResponse()
                    {
                        PreferableSequenceOfCities = _currentSequence.Select(Guid.Parse),
                        CalculatedDistance = graph.GetEdge(_currentSequence[0], _currentSequence[1]).EdgeWeight +
                        graph.GetEdge(_currentSequence[1], _currentSequence[2]).EdgeWeight +
                        graph.GetEdge(_currentSequence[0], _currentSequence[2]).EdgeWeight,
                        NameAlghorithm = nameof(TravelSalesmanAnnealingResolver),
                        ProcessDuration = GetProcessDuration(_timeCounter.Elapsed)
                    };
                    return response;
                }
            }
            while (_temperature >= 0.05)
            {
                var changedIndexes = GetRandomIndexVertices(_minLimit, _maxLimit);
                var randomProbability = GetRandomProbability();
                var changedSequence = SwapVertexSequence(_currentSequence, changedIndexes);
                _currentWeightValue = GetEdgeSum(changedSequence, graph);
                if (CheckExecuting(_currentWeightValue)) return null;
                _deltaWeight = GetDeltaWeight(_currentWeightValue, _previosWeightValue);
                _currentProbability = GetProbability(_temperature, _deltaWeight);
                ChangeTemperature();
                if (ComparePropabilities(randomProbability))
                    MatchSequencesAndWeights(changedSequence);
                _previosWeightValue = _currentWeightValue;
            }
            _timeCounter.Stop();
            response = new TravelSalesmanResponse()
            {
                PreferableSequenceOfCities = _preferableSequnce.Select(Guid.Parse),
                CalculatedDistance = _minWeightValue,
                NameAlghorithm = nameof(TravelSalesmanAnnealingResolver),
                ProcessDuration = GetProcessDuration(_timeCounter.Elapsed)
            };
            return response;
        }
        #region Supporting functionallity
        public string[] SwapVertexSequence(string[] sequence, int[] changedIndexes)
        {
            string buf;
            buf = sequence[changedIndexes[0]];
            sequence[changedIndexes[0]] = sequence[changedIndexes[1]];
            sequence[changedIndexes[1]] = buf;
            return sequence;
        }
        private int GetEdgeSum(string[] currentSequence, Graph graph)
        {
            if (currentSequence.Length == 1 || currentSequence.Length == 2 || currentSequence.Length == 3) return 0;
            int weightValue = 0;
            for (int i = 0; i < graph.Vertices.Count - 1; i++)
            {
                var CurrentEdge = graph.GetEdge(currentSequence[i], currentSequence[i + 1]);
                if (CurrentEdge == null)
                {
                    weightValue += new ShortestPathResolverService().FindShortestPath(_graph, currentSequence[i], currentSequence[i + 1]).FinalDistance;
                }
                else
                {
                    weightValue += CurrentEdge.EdgeWeight;
                }
            }
            var LastEdge = graph.GetEdge(currentSequence[currentSequence.Length - 1], currentSequence[0]);
            if (LastEdge == null)
            {
                weightValue += new ShortestPathResolverService().FindShortestPath(_graph, currentSequence[currentSequence.Length - 1], currentSequence[0]).FinalDistance;
            }
            else
            {
                weightValue += graph.GetEdge(currentSequence[currentSequence.Length - 1], currentSequence[0]).EdgeWeight;
            }

            return weightValue;
        }
        private void ChangeTemperature() => _temperature *= 0.75;

        private double GetProbability(double temperature, double deltaWeight)
        {
            var probability = 100 * Math.Pow(Math.E, (-deltaWeight) / temperature);
            return probability;
        }
        private int[] GetRandomIndexVertices(int initIndex, int lastIndex)
        {
            var rand = new Random();
            var ans = new int[2];
            ans[0] = rand.Next(initIndex, lastIndex);
            ans[1] = rand.Next(initIndex, lastIndex);
            while (ans[0] == ans[1])
            {
                ans[0] = rand.Next(initIndex, lastIndex);
            }
            return ans;
        }

        private double GetRandomProbability()
        {
            var rand = new Random();
            return rand.NextDouble() * 100;
        }

        private double GetDeltaWeight(double currentWeightValue, double previosWeightValue) => currentWeightValue - previosWeightValue;

        private bool ComparePropabilities(double comparablePropability) => _currentProbability > comparablePropability;
        private void MatchSequencesAndWeights(string[] changedSequence)
        {
            if (_currentWeightValue <= _minWeightValue)
            {
                _minWeightValue = _currentWeightValue;
                _preferableSequnce = changedSequence;
            }
        }

        private void Initialize(Graph graph)
        {
            _currentSequence = graph.Vertices.Select(t => t.Name).ToArray();
            _minLimit = _currentSequence.IndexOf(_currentSequence.First());
            _maxLimit = _currentSequence.IndexOf(_currentSequence.Last());
            _minWeightValue = GetEdgeSum(_currentSequence, graph) * 2;
            _preferableSequnce = _currentSequence;
            _timeCounter = new Stopwatch();
            _timeCounter.Start();
        }


        private bool CheckExecuting(double criticalValue) => criticalValue.Equals(0);
        private string GetProcessDuration(TimeSpan timeSpan)
        {
            var seconds = timeSpan.Seconds.ToString();
            var milliSeconds = timeSpan.TotalMilliseconds;
            return $"{seconds}s,{milliSeconds}ms.";
        }
        #endregion
    }
}
