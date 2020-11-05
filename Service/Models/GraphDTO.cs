using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Models
{
    public class GraphDTO
    {
        public IEnumerable<int> Vertexes { get; set; }
        public IEnumerable<EdgeDTO> Edges { get; set; }

        public int VertexCount => Vertexes.Count();
    }
}
