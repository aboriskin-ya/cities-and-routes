using System;

namespace DesktopApp.Models
{
    public class PathModel
    {
        public Guid MapId { get; set; }
        public Guid CityFromId { get; set; }
        public string CityFromName { get; set; }
        public Guid CityToId { get; set; }
        public string CityToName { get; set; }
    }
}
