﻿using System;
using System.Collections.Generic;

namespace Service.DTO
{
    public class ShortestPathResponseDTO
    {
        public bool IsPathFound { get; set; }
        public List<Guid> Path { get; set; }
        public List<string> PathCitiesNames { get; set; }
        public int FinalDistance { get; set; }
        public string ProcessDuration { get; set; }
    }
}
