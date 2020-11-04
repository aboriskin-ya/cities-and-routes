using Repository.Storages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Service
{
    public class TravelSalesmanResolver : ITravelSalesmanResolver
    { 
        public IEnumerable<int> CalcAppropriatePath(IEnumerable<int> SelectedCitiesId) => null;
    }
}
