using System;
using System.Collections.Generic;

namespace Service.PathResolver
{
    public class TravelSalesmanRequest
    {
        public Guid MapId { get; set; }
        public IEnumerable<Guid> SelectedCities { get; set; }
    }
}
