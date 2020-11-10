using Service.Models;
using Service.TSRMethods;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Service
{
    public class TravelSalesmanResolver : ITravelSalesmanResolver
    {

        private AnnealingMethod _AnnealingMethod;
        public TravelSalesmanResolver()
        {
            _AnnealingMethod = new AnnealingMethod();
        }
        public IEnumerable<int> Resolve(IEnumerable<int> Vertexes, GraphDTO Graph)
        {
            return _AnnealingMethod.SolveGoal(Graph);
        }
    }
}
