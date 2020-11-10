using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.TSRMethods
{
    public class AnnealingMethod
    {
        private IEnumerable<int> _PreferableSequnce;
        private double _Temperature = 100;
        private double _DeltaWeight;
        private double _MinWeightValue;
        private double _CurrentProbability;
        private int _MinLimit;
        private int _MaxLimit;
        public void ChangeTemperature() => _Temperature *= 0.75;
        public IEnumerable<int> SolveGoal(GraphDTO graph)
        {
            double WorkWeightValue = 0;
            int[] CurrentSequence = graph.Vertexes.ToArray();
            _MinLimit = CurrentSequence[0];
            _MaxLimit = CurrentSequence[CurrentSequence.Length - 1];
            _MinWeightValue = GetEdgeSum(CurrentSequence, graph);
            var LastWeightValue = _MinWeightValue;
            while (_Temperature >= 0.05)
            {
                var ChangedIndexes = GetRandomVexes(_MinLimit, _MaxLimit);
                var RandomProbability = GetRandomProbability();
                var PrevSequence = SwapVertexSequnce(CurrentSequence.ToArray(), ChangedIndexes);
                WorkWeightValue = GetEdgeSum(PrevSequence, graph);
                _DeltaWeight = WorkWeightValue - LastWeightValue;
                _CurrentProbability = GetProbability(_Temperature, _DeltaWeight);
                if (_CurrentProbability > RandomProbability)
                {
                    CurrentSequence = PrevSequence;
                    if (WorkWeightValue < _MinWeightValue)
                    {
                        _MinWeightValue = WorkWeightValue;
                        _PreferableSequnce = PrevSequence;
                    }
                    LastWeightValue = WorkWeightValue;
                }

            }
            return _PreferableSequnce;

        }

        public int[] SwapVertexSequnce(int[] Sequence, int[] ChangedIndexes)
        {
            for (int i = 0; i < Sequence.Count(); i++)
            {
                if (Sequence[i] == ChangedIndexes[0])
                {
                    Sequence[i] = ChangedIndexes[1];
                    continue;
                }
                if (Sequence[i] == ChangedIndexes[1])
                {
                    Sequence[i] = ChangedIndexes[0];
                    continue;
                }
            }
            return Sequence;
        }
        public double GetEdgeSum(int[] CurrentSequence, GraphDTO graph)
        {
            double MinValue = 0;
            for (int i = 0; i < graph.VertexCount - 1; i++)
            {
                MinValue += graph.Edges.FirstOrDefault(t => t.InitVertex == CurrentSequence[i] && t.EndVertex == CurrentSequence[i + 1]).Distance;
            }
            MinValue += graph.Edges.FirstOrDefault(t => t.InitVertex == CurrentSequence[CurrentSequence.Length - 1] && t.EndVertex == CurrentSequence[0]).Distance;
            return MinValue;
        }

        public double GetProbability(double Temperature, double DeltaWeight)
        {
            var Probability = 100 * Math.Pow(Math.E, (-DeltaWeight) / Temperature);
            ChangeTemperature();
            return Probability;
        }

        public int[] GetRandomVexes(int InitIndex, int LastIndex)
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
