using System.Collections.Generic;

namespace PathResolver
{
    public class Graph
    {
        public List<GraphVertex> Vertices { get; }
        public Graph()
        {
            Vertices = new List<GraphVertex>();
        }

        public void AddVertex(string VertexName)
        {
            Vertices.Add(new GraphVertex(VertexName));
        }

        public GraphVertex FindVertex(string VertexName)
        {
            foreach (var v in Vertices)
            {
                if (v.Name.Equals(VertexName))
                {
                    return v;
                }
            }

            return null;
        }

        public void AddEdge(string FirstName, string SecondName, int Weight)
        {
            var V1 = FindVertex(FirstName);
            var V2 = FindVertex(SecondName);
            if (V2 != null && V1 != null)
            {
                V1.AddEdge(V2, Weight);
                V2.AddEdge(V1, Weight);
            }
        }
    }
}
