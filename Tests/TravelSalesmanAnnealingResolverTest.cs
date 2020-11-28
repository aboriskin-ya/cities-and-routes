using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Moq;
using Repository.Storage;
using Service;
using Service.DTO;
using PathResolver;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Service.PathResolver;

namespace Tests
{
    public class TravelSalesmanAnnealingResolverTest
    {
        private readonly Guid saintPetersburgId = Guid.NewGuid();
        private readonly Guid moscowId = Guid.NewGuid();
        private readonly Guid smolenskId = Guid.NewGuid();
        private readonly Guid voronezhId = Guid.NewGuid();
        private readonly Guid saratovId = Guid.NewGuid();
        private readonly Guid kazanId = Guid.NewGuid();
        private readonly Guid ekaterinburgId = Guid.NewGuid();
        private readonly Guid samaraId = Guid.NewGuid();
        private readonly Guid archangelskId = Guid.NewGuid();
        private readonly Guid rostovOnDonId = Guid.NewGuid();
        private readonly Guid volgogradId = Guid.NewGuid();
        private readonly Guid salehardId = Guid.NewGuid();
        private readonly Guid tomskId = Guid.NewGuid();
        private readonly Guid irkutskId = Guid.NewGuid();
        private readonly Guid surgutId = Guid.NewGuid();
        private readonly Map map;
        private readonly ShortPathResolverDTO testShortPathResolverDTO;
        private readonly Mock<IMapRepository> mockMapRepository;
        private readonly Mock<IPathToGraphService> mockPathToGraphService;
        private readonly AlgorithmService testAlgorithmService;
        private readonly TravelSalesmanAnnealingResolver travelSalesmanAnnealingResolver;
        private readonly Graph graph;

        public TravelSalesmanAnnealingResolverTest()
        {
            map = new Map
            {
                Id = Guid.NewGuid(),
                Cities = new List<City>
                {
                    new City {Name = "SaintPetersburg", Id = saintPetersburgId},
                    new City {Name = "Moscow", Id = moscowId},
                    new City {Name = "Smolensk", Id = smolenskId},
                    new City {Name = "Voronezh", Id = voronezhId},
                    new City {Name = "Saratov", Id = saratovId},
                    new City {Name = "Kazan", Id = kazanId},
                    new City {Name = "Ekaterinburg", Id = ekaterinburgId},
                    new City {Name = "Samara", Id = samaraId},
                    new City {Name = "Archangelsk", Id = archangelskId},
                    new City {Name = "RostovOnDon", Id = rostovOnDonId},
                    new City {Name = "Volgograd", Id = volgogradId},
                    new City {Name = "Salehard", Id = salehardId},
                    new City {Name = "Tomsk", Id = tomskId},
                    new City {Name = "Irkutsk", Id = irkutskId},
                    new City {Name = "Surgut", Id = surgutId}
                },
                Routes = new List<Route>
                {
                    new Route {FirstCityId = saintPetersburgId, SecondCityId = moscowId, Distance = 705},
                    new Route {FirstCityId = smolenskId, SecondCityId = moscowId, Distance = 398},
                    new Route {FirstCityId = voronezhId, SecondCityId = moscowId, Distance = 525},
                    new Route {FirstCityId = smolenskId, SecondCityId = voronezhId, Distance = 690},
                    new Route {FirstCityId = voronezhId, SecondCityId = saratovId, Distance = 513},
                    new Route {FirstCityId = moscowId, SecondCityId = kazanId, Distance = 822},
                    new Route {FirstCityId = saratovId, SecondCityId = kazanId, Distance = 673},
                    new Route {FirstCityId = kazanId, SecondCityId = ekaterinburgId, Distance = 976},
                    new Route {FirstCityId = saratovId, SecondCityId = samaraId, Distance = 424},
                    new Route {FirstCityId = kazanId, SecondCityId = samaraId, Distance = 355},
                    new Route {FirstCityId = samaraId, SecondCityId = ekaterinburgId, Distance = 968},
                    new Route {FirstCityId = saintPetersburgId, SecondCityId = archangelskId, Distance = 1158},
                    new Route {FirstCityId = archangelskId, SecondCityId = ekaterinburgId, Distance = 1877},
                    new Route {FirstCityId = voronezhId, SecondCityId = rostovOnDonId, Distance = 566},
                    new Route {FirstCityId = saratovId, SecondCityId = volgogradId, Distance = 378},
                    new Route {FirstCityId = volgogradId, SecondCityId = rostovOnDonId, Distance = 472},
                    new Route {FirstCityId = kazanId, SecondCityId = archangelskId, Distance = 1422},
                    new Route {FirstCityId = archangelskId, SecondCityId = salehardId, Distance = 1210},
                    new Route {FirstCityId = ekaterinburgId, SecondCityId = salehardId, Distance = 2456},
                    new Route {FirstCityId = ekaterinburgId, SecondCityId = tomskId, Distance = 1849},
                    new Route {FirstCityId = ekaterinburgId, SecondCityId = surgutId, Distance = 1117},
                    new Route {FirstCityId = salehardId, SecondCityId = surgutId, Distance = 1339},
                    new Route {FirstCityId = tomskId, SecondCityId = irkutskId, Distance = 1633}
                }
            };
            testShortPathResolverDTO = new ShortPathResolverDTO() { Cities = map.Cities, Routes = map.Routes };
            travelSalesmanAnnealingResolver = new TravelSalesmanAnnealingResolver();

            graph = new Graph();
            graph.AddVertex(voronezhId.ToString());
            graph.AddVertex(moscowId.ToString());
            graph.AddVertex(smolenskId.ToString());
            graph.AddVertex(kazanId.ToString());
            graph.AddEdge(voronezhId.ToString(), smolenskId.ToString(), 690);
            graph.AddEdge(voronezhId.ToString(), kazanId.ToString(), 1057);
            //graph.AddEdge(voronezhId.ToString(), moscowId.ToString(), 525);
            graph.AddEdge(moscowId.ToString(), smolenskId.ToString(), 398);
            graph.AddEdge(moscowId.ToString(), kazanId.ToString(), 822);
            //graph.AddEdge(smolenskId.ToString(), kazanId.ToString(), 1223);

        }
        [Fact]
        public void CheckSalesmanAnnealingVoronezSmolenskMoscow()
        {
            //Arrange
            TravelSalesmanResponse expectedResult = new TravelSalesmanResponse
            { CalculatedDistance = 2967, PreferableSequenceOfCities = new List<Guid> { voronezhId, smolenskId, moscowId, kazanId } };

            //Act
            var result = travelSalesmanAnnealingResolver.Resolve(graph);
            //Assert
            Assert.Equal(expectedResult.CalculatedDistance, result.CalculatedDistance);
            foreach (var city in expectedResult.PreferableSequenceOfCities)
            {
                Assert.Contains(city, result.PreferableSequenceOfCities);
            }
        }
    }
}
