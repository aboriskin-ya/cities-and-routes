using Microsoft.EntityFrameworkCore.Internal;
using PathResolver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.TSRMethods
{
    public class AnnealingMethod
    {
        private IEnumerable<string> _PreferableSequnce;
        private double _Temperature=100;
        private double _DeltaWeight;
        private double _MinWeightValue;
        private double _CurrentProbability;
        private int _MinLimit;
        private int _MaxLimit;
        public void ChangeTemperature() => _Temperature *= 0.75;
        public IEnumerable<string> SolveGoal(Graph graph)
        {
            double WorkWeightValue = 0;
            string[] CurrentSequence = graph.Vertices.Select(t => t.Name).ToArray();
            _MinLimit = CurrentSequence.IndexOf(CurrentSequence.First());
            _MaxLimit = CurrentSequence.IndexOf(CurrentSequence.Last());
            _MinWeightValue = GetEdgeSum(CurrentSequence,graph);
            if (_MinWeightValue.Equals(double.MaxValue)) return null;
            var LastWeightValue = _MinWeightValue;
            _PreferableSequnce = CurrentSequence;
            while(_Temperature>=0.05)
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
            double MinValue = 0;
            
                for (int i = 0; i < graph.Vertices.Count - 1; i++)
                {
                    try
                    {
                        MinValue += graph.Edges.FirstOrDefault(t => t.FirstVertex?.Name == CurrentSequence[i]
                        && t.SecondVertex?.Name == CurrentSequence[i + 1])?.EdgeWeight ?? throw new NullReferenceException();
                    }
                    catch (NullReferenceException)
                    {
                        return double.MaxValue;
                    }
                }
                MinValue += graph.Edges.FirstOrDefault(t => t.FirstVertex.Name == CurrentSequence[CurrentSequence.Length - 1]
                && t.SecondVertex.Name == CurrentSequence[0]).EdgeWeight;
            
           
            return MinValue;
        }

        public double GetProbability(double Temperature,double DeltaWeight)
        {
            var Probability = 100 * Math.Pow(Math.E, (-DeltaWeight)/Temperature);
            ChangeTemperature();
            return Probability;
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
