using Microsoft.EntityFrameworkCore.Internal;
using PathResolver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.TSRMethods
{
    public class AnnealingMethod
    {
        private IEnumerable<string> _preferableSequnce;
        private double _temperature=100;
        private double _deltaWeight;
        private double _minWeightValue;
        private double _currentProbability;
        private int _minLimit;
        private int _maxLimit;
        public void ChangeTemperature() => _temperature *= 0.75;
        public IEnumerable<string> SolveGoal(Graph graph)
        {
            double workWeightValue = 0;
            string[] currentSequence = graph.Vertices.Select(t => t.Name).ToArray();
            _minLimit = currentSequence.IndexOf(currentSequence.First());
            _maxLimit = currentSequence.IndexOf(currentSequence.Last());
            _minWeightValue = GetEdgeSum(currentSequence,graph);
            if (_minWeightValue.Equals(double.MaxValue)) return null;
            var lastWeightValue = _minWeightValue;
            _preferableSequnce = currentSequence;
            while(_temperature>=0.05)
            {
                var changedIndexes = GetRandomVexes(_minLimit, _maxLimit);
                var randomProbability = GetRandomProbability();
                var prevSequence = SwapVertexSequnce(currentSequence, changedIndexes);
                workWeightValue = GetEdgeSum(prevSequence, graph);
                _deltaWeight = workWeightValue - lastWeightValue;
                _currentProbability = GetProbability(_temperature, _deltaWeight);
                if (_currentProbability > randomProbability)
                {
                    currentSequence = prevSequence;
                    if (workWeightValue < _minWeightValue)
                    {
                        _minWeightValue = workWeightValue;
                        _preferableSequnce = prevSequence;
                    }
                    lastWeightValue = workWeightValue;
                }
                
            }
            return _preferableSequnce;
        }

        public string[] SwapVertexSequnce(string[] Sequence,int[]ChangedIndexes)
        {
            string buf;
            buf = Sequence[ChangedIndexes[0]];
            Sequence[ChangedIndexes[0]] = Sequence[ChangedIndexes[1]];
            Sequence[ChangedIndexes[1]] = buf;
            return Sequence;
        }
        public double GetEdgeSum(string[]CurrentSequence,Graph graph)
        {
            double minValue = 0;
                for (int i = 0; i < graph.Vertices.Count - 1; i++)
                {
                    var CurrentEdge = graph.GetEdge(CurrentSequence[i], CurrentSequence[i + 1]);
                    if (CurrentEdge == null) return double.MaxValue;
                    minValue += CurrentEdge.EdgeWeight;
                }
             minValue += graph.GetEdge(CurrentSequence[CurrentSequence.Length - 1], CurrentSequence[0]).EdgeWeight;
             return minValue;
        }

        public double GetProbability(double Temperature,double DeltaWeight)
        {
            var probability = 100 * Math.Pow(Math.E, (-DeltaWeight)/Temperature);
            ChangeTemperature();
            return probability;
        }

        public int[] GetRandomVexes(int InitIndex,int LastIndex)
        {
            var rand = new Random();
            var ans = new int[2];
            ans[0] = rand.Next(InitIndex, LastIndex);
            ans[1] = rand.Next(InitIndex, LastIndex);
            while (ans[0] == ans[1])
            {
                ans[0] = rand.Next(InitIndex, LastIndex);
            }
            return ans;
        }
        
        public double GetRandomProbability()
        {
            var rand = new Random();
            return rand.NextDouble() * 100;
        }
    }
}
