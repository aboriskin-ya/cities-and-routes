using Repository.Storages;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Service
{
    public class TravelSalesmanResolver : ITravelSalesmanResolver
    { 
        public IEnumerable<int> Resolve(IEnumerable<int> Vertexes, GraphDTO Graph) => null;
    }
}
