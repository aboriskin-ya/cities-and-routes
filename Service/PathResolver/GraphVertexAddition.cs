using PathResolver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.PathResolver
{
    class GraphVertexAddition
    {
        public bool IsUnvisited { get; set; }
        public int EdgesWeightSum { get; set; }
        public GraphVertex PreviousVertex { get; set; }
        public List<GraphVertex> NextVertices { get; set; }
    }
}
