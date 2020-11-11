using System;
using System.Collections.Generic;

namespace Service.PathResolver
{
    public class TravelSalesmanResponse
    {
        public IEnumerable<Guid> PreferableSequenceOfCities { get; set; }

        public string NameAlghorithm { get; set; }

        public int CalculatedDistance { get; set; }
    }
}
