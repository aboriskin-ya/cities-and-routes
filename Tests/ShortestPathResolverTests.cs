using Moq;
using Service.PathResolver;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Service;
using Service.Services;
using Service.DTO;
using DataAccess.Models;
using Microsoft.Extensions.Logging;
using Repository.Storage;

namespace Tests
{
    public class ShortestPathResolverTests
    {
        Guid saintPetersburgId = new Guid("9ce1a4e6-4d54-4b5e-c5c0-08d88d38ebe9");
        Guid moscowId = new Guid("d68e528b-f2b3-4e34-c5c1-08d88d38ebe9");
        Guid smolenskId = new Guid("6561eca6-ebbf-4da6-c5c2-08d88d38ebe9");
        Guid voronezhId = new Guid("5c08ec62-a464-414e-c5c3-08d88d38ebe9");
        Guid saratovId = new Guid("53c3b16c-616e-4395-c5c4-08d88d38ebe9");
        Guid kazanId = new Guid("36dfc7c6-54df-4b9f-c5c5-08d88d38ebe9");
        Guid ekaterinburgId = new Guid("d3a73ffc-70b4-4888-c5c6-08d88d38ebe9");
        Guid samaraId = new Guid("8cfe948b-912c-4e37-c5c7-08d88d38ebe9");
        Guid archangelskId = new Guid("d01669eb-e5dc-489e-c5c8-08d88d38ebe9");
        Guid rostovOnDonId = new Guid("86d39825-3c5c-4a4e-c5c9-08d88d38ebe9");
        Guid volgogradId = new Guid("85884386-ae41-4354-c5ca-08d88d38ebe9");
        Guid mapId = new Guid("e6efe688-c2ed-4ce7-2aed-08d88d38c2ca");
        [Fact]     
        public void CheckShortestPath()
        {
            //Arrange
            Map map = new Map { Id = mapId };       
            map.Cities = new List<City>
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
                new City {Name = "Volgograd", Id = volgogradId}
            };
            map.Routes = new List<Route>
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
                new Route {FirstCityId = kazanId, SecondCityId = archangelskId, Distance = 1422}
            };
            ShortPathResolverDTO testShortPathResolverDTO = new ShortPathResolverDTO { Cities = new List<City>(map.Cities), Routes = new List<Route>(map.Routes) };

            List<Guid> expectedResultPath = new List<Guid>
            {
                rostovOnDonId, //RostovOnDon
                voronezhId, //Voronezh 
                moscowId, //Moscow
                saintPetersburgId //SaintPetersburg
            };
            int expectedResultDistance = 1796;

            var mockMapRepository = new Mock<IMapRepository>();
            mockMapRepository.Setup(_mapRepository => _mapRepository.GetWholeMap(new Guid("e6efe688-c2ed-4ce7-2aed-08d88d38c2ca"))).Returns(map);
            var mockPathToGraphService = new Mock<IPathToGraphService>();
            mockPathToGraphService.Setup(_pathToGraphService => _pathToGraphService.MapToResolver(map))
                .Returns(testShortPathResolverDTO);
            AlgorithmService testAlgorithmService = new AlgorithmService(mockMapRepository.Object, null, mockPathToGraphService.Object, 
                null, null, null, new Logger<AlgorithmService>(new LoggerFactory()));
            //Act
            //Path from Rostov to SaintPetersburg
            var result = testAlgorithmService.FindShortestPath(map.Id, rostovOnDonId, saintPetersburgId);
            //Assert
            Assert.Equal(expectedResultPath, result.Path);
            Assert.Equal(expectedResultDistance, result.FinalDistance);
        }
    }
}
