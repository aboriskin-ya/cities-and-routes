using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathResolver
{
    public class GraphVertexInfo
    {
        public GraphVertex Vertex { get; set; }

        public bool IsUnvisited { get; set; }

        public int EdgesWeightSum { get; set; }

        public GraphVertex PreviousVertex { get; set; }

        public GraphVertexInfo(GraphVertex vertex)
        {
            Vertex = vertex;
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousVertex = null;
        }
    }
}
