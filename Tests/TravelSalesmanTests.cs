using Xunit;
using DataAccess.DTO;
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
            var SelectedCities = new int[] { 6, 7, 8 };
            var resolver = new TravelSalesmanResolver();
            var graph = new GraphDTO();
            var EdgeList = new List<EdgeDTO>()
            {
                 new EdgeDTO{ Distance=500 },
                 new EdgeDTO{ Distance=123 },
                 new EdgeDTO{ Distance=145 },
            };
            graph.Edges = EdgeList;
            graph.Vertexes = new int[] { 7, 8, 6 };
            var ExptectedAns = new int[] { 8, 6, 7};
            //Act
            var ActualAnswer = resolver.CalcAppropriatePath(SelectedCities);
            //Assert
            Assert.Equal(ExptectedAns, ActualAnswer);

        }
        [Fact]
        public void CheckTravelSalesman5Cities()
        {
            //Arrange
            var SelectedCities = new int[] { 1, 2, 3, 4, 5 };
            var resolver = new TravelSalesmanResolver();
            var graph = new GraphDTO();
            var EdgeList = new List<EdgeDTO>()
            {
                 new EdgeDTO{ Distance=435 },
                 new EdgeDTO{ Distance=322 },
                 new EdgeDTO{ Distance=631 },
                 new EdgeDTO{ Distance=320 },
                 new EdgeDTO{ Distance=160 }
            };
            graph.Edges = EdgeList;
            graph.Vertexes = new int[] { 1,2,3,4,5 };
            var ExptectedAns = new int[] {5,3,2,4,1 };
            //Act
            var ActualAnswer = resolver.CalcAppropriatePath(SelectedCities);
            //Assert
            Assert.Equal(ExptectedAns, ActualAnswer);

        }
    }
}
