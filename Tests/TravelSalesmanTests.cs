using Xunit;
using Service.Models;
using System.Collections.Generic;
using Service;
using System.Net.Http;

namespace Tests
{
    public class TravelSalesmanTests
    {
        [Fact]
        public void CheckTravelSalesman3Cities()
        {
            //Arrange
            var Vertexes = new int[] { 6, 7, 8 };
            var resolver = new TravelSalesmanResolver();
            var graph = new GraphDTO();
            var EdgeList = new List<EdgeDTO>()
            {
                 new EdgeDTO{ Distance=500,InitVertex=6,EndVertex=7 },
                 new EdgeDTO{ Distance=123,InitVertex=7,EndVertex=8 },
                 new EdgeDTO{ Distance=145,InitVertex=8,EndVertex=6 },
            };
            graph.Edges = EdgeList;
            graph.Vertexes = new int[] { 7, 8, 6 };
            var ExptectedAns = new int[] { 8, 6, 7};
            //Act
            var ActualAnswer = resolver.Resolve(Vertexes,graph);
            //Assert
            Assert.Equal(ExptectedAns, ActualAnswer);

        }
        [Fact]
        public void CheckTravelSalesman5Cities()
        {
            //Arrange
            var Vertexes = new int[] { 1, 2, 3, 4, 5 };
            var resolver = new TravelSalesmanResolver();
            var graph = new GraphDTO();
            var EdgeList = new List<EdgeDTO>()
            {
                 new EdgeDTO{ Distance=435,InitVertex=1,EndVertex=2 },
                 new EdgeDTO{ Distance=322,InitVertex=2,EndVertex=3 },
                 new EdgeDTO{ Distance=631,InitVertex=3,EndVertex=4 },
                 new EdgeDTO{ Distance=320,InitVertex=4,EndVertex=5 },
                 new EdgeDTO{ Distance=160,InitVertex=5,EndVertex=1 }
            };
            graph.Edges = EdgeList;
            graph.Vertexes = new int[] { 1,2,3,4,5 };
            var ExptectedAns = new int[] {5,3,2,4,1 };
            //Act
            var ActualAnswer = resolver.Resolve(Vertexes,graph);
            //Assert
            Assert.Equal(ExptectedAns, ActualAnswer);

        }
    }
}
