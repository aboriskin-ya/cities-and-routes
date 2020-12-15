using System;
using System.Collections.Generic;

namespace DesktopApp.Models
{
    public class TravelSalesman
    {
        public List<Guid> PreferableSequenceOfCities { get; set; }
        public string NameAlghorithm { get; set; }
        public double CalculatedDistance { get; set; }
        public string ProcessDuration { get; set; }
    }
}
