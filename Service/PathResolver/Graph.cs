using System.Collections.Generic;

namespace PathResolver
{
    public class Graph
    {
        public List<GraphVertex> Vertices { get; }

        public List<GraphEdge> Edges { get;  }
        public Graph()
        {
            Vertices = new List<GraphVertex>();
            Edges = new List<GraphEdge>();
        }

        public void AddVertex(string VertexName)
        {
            Vertices.Add(new GraphVertex(VertexName));
        }

        public GraphEdge GetEdge(string FirstVertexName,string SecondVertexName)
        {
            foreach(var item in Edges)
            {
                if (FirstVertexName.Equals(item.FirstVertex.Name) && SecondVertexName.Equals(item.SecondVertex.Name)) return item;
            }
            return null;
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
            Edges.Add(new GraphEdge(FindVertex(FirstName), FindVertex(SecondName), Weight));
            Edges.Add(new GraphEdge(FindVertex(SecondName), FindVertex(FirstName), Weight));
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
