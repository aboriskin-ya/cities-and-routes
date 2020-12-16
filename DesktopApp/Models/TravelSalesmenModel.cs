using System;
using System.Collections.Generic;

namespace DesktopApp.Models
{
    public class TravelSalesmanModel
    {
        public Guid MapId { get; set; }
        public List<Guid> SelectedCities { get; set; }
    }
}
