using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.Models
{
    public class TravelSalesman
    {
        public IEnumerable<Guid> PreferableSequenceOfCities { get; set; }
        public string NameAlghorithm { get; set; }
        public double CalculatedDistance { get; set; }
        public string ProcessDuration { get; set; }
    }
}
