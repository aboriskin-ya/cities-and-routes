using PathResolver;
using Service.PathResolver;

namespace Service.Services.Interfaces
{
    public interface ITravelSalesmanNearestNeighbor
    {
        TravelSalesmanResponse Solve(Graph graph);
    }
}
