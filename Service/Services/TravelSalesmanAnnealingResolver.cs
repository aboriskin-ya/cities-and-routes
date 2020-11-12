using Microsoft.EntityFrameworkCore.Internal;
using PathResolver;
using Service.PathResolver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Service
{
    public class TravelSalesmanAnnealingResolver : ITravelSalesmanAnnealingResolver
    {
        #region fields
        private double _result;
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
        #endregion
        public TravelSalesmanResponse Resolve(Graph graph)
        {
            Initialize(graph);
            if (CheckExecuting(_minWeightValue)) return null;
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
            var response = new TravelSalesmanResponse()
            {
                PreferableSequenceOfCities = _preferableSequnce.Select(Guid.Parse),
                CalculatedDistance = _result,
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
            if (currentSequence.Length == 1) return int.MaxValue;
            int weightValue = 0;
            for (int i = 0; i < graph.Vertices.Count - 1; i++)
            {
                var CurrentEdge = graph.GetEdge(currentSequence[i], currentSequence[i + 1]);
                if (CurrentEdge == null) return int.MaxValue;
                weightValue += CurrentEdge.EdgeWeight;
            }
            weightValue += graph.GetEdge(currentSequence[currentSequence.Length - 1], currentSequence[0]).EdgeWeight;
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
            if (_currentWeightValue < _minWeightValue)
            {
                _minWeightValue = _currentWeightValue;
                _result += _minWeightValue;
                _preferableSequnce = changedSequence;
            }
        }

        private void Initialize(Graph graph)
        {
            _currentSequence = graph.Vertices.Select(t => t.Name).ToArray();
            _minLimit = _currentSequence.IndexOf(_currentSequence.First());
            _maxLimit = _currentSequence.IndexOf(_currentSequence.Last());
            _minWeightValue = GetEdgeSum(_currentSequence, graph);
            _preferableSequnce = _currentSequence;
            _timeCounter = new Stopwatch();
            _timeCounter.Start();
        }
        

        private bool CheckExecuting(double criticalValue) => criticalValue.Equals(double.MaxValue);
        private string GetProcessDuration(TimeSpan timeSpan)
        {
            var seconds = timeSpan.Seconds.ToString();
            var milliSeconds = timeSpan.Milliseconds;
            return $"{seconds}s,{milliSeconds}ms.";
        }
        #endregion
    }
}
