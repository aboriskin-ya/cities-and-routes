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

        public GraphVertex(string vertexName)
        {
            Name = vertexName;
            Edges = new List<GraphEdge>();
        }
        
        public void AddEdge(GraphEdge newEdge)
        {
            Edges.Add(newEdge);
        }
        
        public void AddEdge(GraphVertex vertex, int edgeWeight)
        {
            if (!FindEdge(vertex.Name))
            {
                AddEdge(new GraphEdge(vertex, edgeWeight));
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
