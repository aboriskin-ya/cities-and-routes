using Service.Models;
using Service.TSRMethods;
using System.Collections.Generic;

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
