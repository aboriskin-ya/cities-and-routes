using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathResolver
{
    public class GraphEdge
    {
        public GraphVertex ConnectedVertex { get; }

        public int EdgeWeight { get; }

        public GraphEdge(GraphVertex ConnectedVertex, int Weight)
        {
            this.ConnectedVertex = ConnectedVertex;
            EdgeWeight = Weight;
        }
    }
}
