using System.Collections.Generic;

namespace PathResolver
{
    public class GraphVertex
    {
        public string Name { get; }
        public List<GraphEdge> Edges { get; }
        public bool IsUnvisited { get; set; }
        public int EdgesWeightSum { get; set; }
        public GraphVertex PreviousVertex { get; set; }

        public GraphVertex(string VertexName)
        {
            Name = VertexName;
            Edges = new List<GraphEdge>();
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousVertex = null;
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
            foreach (var Edge in Edges)
            {
                if (Edge.ConnectedVertex.Name.Equals(EdgeName))
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString() => Name;
    }
}
