using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PathResolver
{
    public class GraphVertex
    {
        public string Name { get; }

        public List<GraphEdge> Edges { get; }

        public GraphVertex(string VertexName)
        {
            Name = VertexName;
            Edges = new List<GraphEdge>();
        }
        
        public void AddEdge(GraphEdge NewEdge)
        {
            Edges.Add(NewEdge);
        }
        
        public void AddEdge(GraphVertex Vertex, int EdgeWeight)
        {
            if (!FindEdge(Vertex.Name))
            {
                AddEdge(new GraphEdge(Vertex, EdgeWeight));
            }
        }

        public bool FindEdge(string EdgeName)
        {
            foreach (var v in Edges)
            {
                if (v.ConnectedVertex.Name.Equals(EdgeName))
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString() => Name;
    }
}
