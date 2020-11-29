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

        public List<GraphVertex> NextVertices { get; }

        public GraphVertex(string VertexName)
        {
            Name = VertexName;
            Edges = new List<GraphEdge>();
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousVertex = null;
            NextVertices = new List<GraphVertex>();
        }
        public void AddNextVertex(GraphVertex secondVertex)
        {
            secondVertex.PreviousVertex = this;
            NextVertices.Add(secondVertex);
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
        public void AddEdge(GraphVertex firstVertex, GraphVertex secondVertex, int weight)
        {
            Edges.Add(new GraphEdge(firstVertex, secondVertex, weight));
        }
        public GraphEdge GetEdge(GraphVertex firstVertex, GraphVertex secondVertex)
        {
            foreach (var edge in Edges)
            {
                if (edge.FirstVertex == null) continue;
                else
                {
                    if (edge.FirstVertex == firstVertex && edge.SecondVertex == secondVertex) return edge;
                }
            }
            return default;
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
