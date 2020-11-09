using System;
using System.Collections.Generic;
using System.Text;

namespace Service.PathResolver
{
    public class AppropriateRouteBody
    {
        public Guid MapId { get; set; }
        public IEnumerable<Guid> SelectedCities { get; set; }
    }
}
