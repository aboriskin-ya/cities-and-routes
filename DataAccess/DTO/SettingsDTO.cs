using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DTO
{
    public class SettingsDTO
    {
        public bool DisplayingImage { get; set; }
        public bool DisplayingGraph { get; set; }
        public double VertexSize { get; set; }
        public string VertexColor { get; set; }
        public double EdgeSize { get; set; }
        public string EdgeColor { get; set; }
    }
}
