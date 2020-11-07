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
            foreach (var Vertex in Vertices)
            {
                if (Vertex.Name.Equals(VertexName))
                {
                    return Vertex;
                }
            }

            return null;
        }

        public void AddEdge(string FirstName, string SecondName, int Weight)
        {
            var FirstVertex = FindVertex(FirstName);
            var SecondVertex = FindVertex(SecondName);
            if (SecondVertex != null && FirstVertex != null)
            {
                FirstVertex.AddEdge(SecondVertex, Weight);
                SecondVertex.AddEdge(FirstVertex, Weight);
            }
        }
    }
}
