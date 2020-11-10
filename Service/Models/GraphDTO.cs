using System.Collections.Generic;
using System.Linq;

namespace Service.Models
{
    public class GraphDTO
    {
        public IEnumerable<int> Vertexes { get; set; }
        public IEnumerable<EdgeDTO> Edges { get; set; }

        public int VertexCount => Vertexes.Count();
    }
}
