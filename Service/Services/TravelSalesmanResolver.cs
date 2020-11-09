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
        private Dictionary<int, Guid> IdDictionary;
        private AnnealingMethod _AnnealingMethod;
        public TravelSalesmanResolver()
        {
            _AnnealingMethod = new AnnealingMethod();
            IdDictionary = new Dictionary<int, Guid>();
        }
        public IEnumerable<Guid> Resolve(IEnumerable<Guid> Vertices, ShortPathResolverDTO CitiesRoutes)
        {
            var keys=_AnnealingMethod.SolveGoal(ConvertToGraph(Vertices,CitiesRoutes)).ToArray();
            for (int i = 0; i < keys.Count(); i++)
            {
                yield return IdDictionary[keys[i]];
            }
        }

        private Graph ConvertToGraph(IEnumerable<Guid> Vertices, ShortPathResolverDTO CitiesRoutes)
        {
            var graph = new Graph();
            var Edges = new List<GraphEdge>();
            for (int i = 0; i < CitiesRoutes.Cities.Count; i++)
            {
                IdDictionary.Add(i, CitiesRoutes.Cities[i].Id);
                graph.AddVertex(i.ToString());
            }
            for(int i = 0; i < CitiesRoutes.Routes.Count-1; i++)
            {
                Edges.Add(new GraphEdge(graph.Vertices[i], graph.Vertices[i + 1], CitiesRoutes.Routes[i].Distance));
                Edges.Add(new GraphEdge(graph.Vertices[i + 1], graph.Vertices[i], CitiesRoutes.Routes[i].Distance));
            }
            graph.Edges = Edges;
            return graph;
        }

        
    }
}
