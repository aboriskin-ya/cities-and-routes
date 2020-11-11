namespace PathResolver
{
    public class GraphEdge
    {
        public GraphVertex ConnectedVertex { get; }

        public int EdgeWeight { get; }
        public GraphVertex FirstVertex { get; set; }
        public GraphVertex SecondVertex { get; set; }
        public GraphEdge(GraphVertex ConnectedVertex, int Weight)
        {
            this.ConnectedVertex = ConnectedVertex;
            EdgeWeight = Weight;
        }
        public GraphEdge(GraphVertex FirstVert, GraphVertex SecondVert, int Weight)
        {
            ConnectedVertex = FirstVert;
            FirstVertex = FirstVert;
            SecondVertex = SecondVert;
            EdgeWeight = Weight;
        }
    }
}
