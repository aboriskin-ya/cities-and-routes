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
        public void CheckTravelSalesman4Cities()
        {
            //Arrange
            
            var resolver = new TravelSalesmanResolver();
            var Vertexes = new int[] { 6, 7, 8, 9 };
            var graph = new GraphDTO();
            var EdgeList = new List<EdgeDTO>()
            {
                 new EdgeDTO{ Distance=13,InitVertex=6,EndVertex=7 },
                 new EdgeDTO{ Distance=13,InitVertex=7,EndVertex=6 },
                 new EdgeDTO{ Distance=5,InitVertex=7,EndVertex=8 },
                 new EdgeDTO{ Distance=5,InitVertex=8,EndVertex=7 },
                 new EdgeDTO{ Distance=7,InitVertex=8,EndVertex=9 },
                 new EdgeDTO{ Distance=7,InitVertex=9,EndVertex=8 },
                 new EdgeDTO{Distance=4,InitVertex=9,EndVertex=6},
                 new EdgeDTO{Distance=4,InitVertex=6,EndVertex=9},
                 new EdgeDTO{Distance=11,InitVertex=6,EndVertex=8},
                 new EdgeDTO{Distance=11,InitVertex=8,EndVertex=6},
                 new EdgeDTO{Distance=4,InitVertex=7,EndVertex=9},
                 new EdgeDTO{Distance=4,InitVertex=9,EndVertex=7}
            };
            graph.Edges = EdgeList;
            graph.Vertexes = Vertexes;
            var ExpectedAns = new int[] { 7, 8, 6, 9 };
            //Act
            var ActualAnswer = resolver.Resolve(Vertexes,graph);
            //Assert
            Assert.Equal(ExpectedAns, ActualAnswer);

        }
        [Fact]
        public void CheckTravelSalesman5Cities()
        {
            //Arrange
            var resolver = new TravelSalesmanResolver();
            var Vertexes = new int[] { 1, 2, 3, 4, 5 };
            var graph = new GraphDTO();
            var EdgeList = new List<EdgeDTO>()
            {
                 new EdgeDTO{ Distance=13,InitVertex=1,EndVertex=2 },
                 new EdgeDTO{ Distance=13,InitVertex=2,EndVertex=1 },
                 new EdgeDTO{ Distance=8,InitVertex=2,EndVertex=3 },
                 new EdgeDTO{ Distance=8,InitVertex=3,EndVertex=2 },
                 new EdgeDTO{ Distance=20,InitVertex=3,EndVertex=4 },
                 new EdgeDTO{ Distance=20,InitVertex=4,EndVertex=3 },
                 new EdgeDTO{ Distance=14,InitVertex=4,EndVertex=5 },
                 new EdgeDTO{ Distance=14,InitVertex=5,EndVertex=4 },
                 new EdgeDTO{ Distance=16,InitVertex=5,EndVertex=1 },
                 new EdgeDTO{ Distance=16,InitVertex=1,EndVertex=5 },
                 new EdgeDTO{ Distance=13,InitVertex=1,EndVertex=3 },
                 new EdgeDTO{ Distance=13,InitVertex=3,EndVertex=1 },
                 new EdgeDTO{ Distance=20,InitVertex=1,EndVertex=4 },
                 new EdgeDTO{ Distance=20,InitVertex=4,EndVertex=1 },
                 new EdgeDTO{ Distance=15,InitVertex=5,EndVertex=2 },
                 new EdgeDTO{ Distance=15,InitVertex=2,EndVertex=5 },
                 new EdgeDTO{ Distance=17,InitVertex=3,EndVertex=5 },
                 new EdgeDTO{ Distance=17,InitVertex=5,EndVertex=3 },
                 new EdgeDTO{ Distance=10,InitVertex=4,EndVertex=2 },
                 new EdgeDTO{ Distance=10,InitVertex=2,EndVertex=4 }
            };
            graph.Edges = EdgeList;
            graph.Vertexes = new int[] { 1,2,3,4,5 };
            var ExpectedAns = new int[] {1,3,2,4,5 };
            //Act
            var ActualAnswer = resolver.Resolve(Vertexes,graph);
            //Assert
            Assert.Equal(ExpectedAns, ActualAnswer);

        }
    }
}
