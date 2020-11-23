using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Service.DTO
{
    public class ShortestPathResponseDTO
    {
        public List<Guid> Path { get; set; }
        public List<string> PathCitiesNames { get; set; }
        public int FinalDistance { get; set; }
        public string ProcessDuration { get; set; }
    }
}
