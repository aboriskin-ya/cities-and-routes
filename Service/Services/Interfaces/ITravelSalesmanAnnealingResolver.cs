using PathResolver;
using Service.PathResolver;

namespace Service
{
    public interface ITravelSalesmanAnnealingResolver
    {
        public TravelSalesmanResponse Resolve(Graph Graph);
    }
}
