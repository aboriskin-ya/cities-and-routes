using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Map Map { get; set; }
        public Guid MapId { get; set; }
        public List<Route> RoutesWhenThisFirst { get; set; }
        public List<Route> RoutesWhenThisSecond { get; set; }
    }
}