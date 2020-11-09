using DataAccess.DTO;
using PathResolver;
using Service.DTO;
using Service.TSRMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class TravelSalesmanResolver : ITravelSalesmanResolver
    {
        private AnnealingMethod _AnnealingMethod;
        public TravelSalesmanResolver()
        {
            _AnnealingMethod = new AnnealingMethod();
        }
        public IEnumerable<Guid> Resolve(IEnumerable<Guid> Vertices, ShortPathResolverDTO CitiesRoutes)
        {
            try
            {
                return _AnnealingMethod.SolveGoal(ConvertToGraph(Vertices, CitiesRoutes)).Select(Guid.Parse);
            }
            catch (Exception)
            {
                return default;
            }
            
        }

        private Graph ConvertToGraph(IEnumerable<Guid> Vertices, ShortPathResolverDTO CitiesRoutes)
        {
            var graph = new Graph();
            foreach (var Vertex in Vertices)
            {
                graph.AddVertex(Vertex.ToString());
            }
            foreach (var route in CitiesRoutes.Routes)
            {
                graph.AddEdge(route.FirstCityId.ToString(), route.SecondCityId.ToString(), route.Distance);
            }
            return graph;
        }

        
    }
}
