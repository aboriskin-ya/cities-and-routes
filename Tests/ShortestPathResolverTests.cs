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
        private readonly Map map;
        private readonly ShortPathResolverDTO testShortPathResolverDTO;
        private readonly Mock<IMapRepository> mockMapRepository;
        private readonly Mock<IPathToGraphService> mockPathToGraphService;
        private readonly AlgorithmService testAlgorithmService;

        public ShortestPathResolverTests()
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
                    new City {Name = "Volgograd", Id = volgogradId}
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
                    new Route {FirstCityId = kazanId, SecondCityId = archangelskId, Distance = 1422}
                }
            };
            testShortPathResolverDTO = new ShortPathResolverDTO
            { Cities = new List<City>(map.Cities), Routes = new List<Route>(map.Routes) };

            mockMapRepository = new Mock<IMapRepository>();
            mockMapRepository.Setup(_mapRepository => _mapRepository.GetWholeMap(map.Id)).Returns(map);
            mockPathToGraphService = new Mock<IPathToGraphService>();
            mockPathToGraphService.Setup(_pathToGraphService => _pathToGraphService.MapToResolver(map))
                .Returns(testShortPathResolverDTO);
            testAlgorithmService = new AlgorithmService(mockMapRepository.Object, null, mockPathToGraphService.Object,
                    null, null, null, new Logger<AlgorithmService>(new LoggerFactory()));

        }
        [Fact]     
        public void CheckShortestPathFromRostovToSaintPetersburg()
        {
            //Arrange
            var expectedResultFromRostovToSaintPetersburg = new ShortestPathResponseDTO
            {
                Path = new List<Guid> { rostovOnDonId, voronezhId, moscowId, saintPetersburgId },
                FinalDistance = 1796
            };                   
            //Act
            var resultFromRostovToSaintPetersburg = testAlgorithmService.FindShortestPath(map.Id, rostovOnDonId, saintPetersburgId);
            //Assert
            Assert.Equal(expectedResultFromRostovToSaintPetersburg.Path, resultFromRostovToSaintPetersburg.Path);
            Assert.Equal(expectedResultFromRostovToSaintPetersburg.FinalDistance, resultFromRostovToSaintPetersburg.FinalDistance);
        }
        [Fact]
        public void CheckShortestPathFromSmolenskToEkaterinburg()
        {
            //Arrange
            var expectedResultFromSmolenskToEkaterinburg = new ShortestPathResponseDTO
            {
                Path = new List<Guid> { smolenskId, moscowId, kazanId, ekaterinburgId },
                FinalDistance = 2196
            };
            //Act
            var resultFromSmolenskToEkaterinburg = testAlgorithmService.FindShortestPath(map.Id, smolenskId, ekaterinburgId);
            //Assert
            Assert.Equal(expectedResultFromSmolenskToEkaterinburg.Path, resultFromSmolenskToEkaterinburg.Path);
            Assert.Equal(expectedResultFromSmolenskToEkaterinburg.FinalDistance, resultFromSmolenskToEkaterinburg.FinalDistance);
        }
        [Fact]
        public void CheckShortestPathFromArchangelskToVolgograd()
        {
            //Arrange
            var expectedResultFromArchangelskToVolgograd = new ShortestPathResponseDTO
            {
                Path = new List<Guid> { archangelskId, kazanId, saratovId, volgogradId },
                FinalDistance = 2473
            };
            //Act
            var resultFromArchangelskToVolgograd = testAlgorithmService.FindShortestPath(map.Id, archangelskId, volgogradId);
            //Assert
            Assert.Equal(expectedResultFromArchangelskToVolgograd.Path, resultFromArchangelskToVolgograd.Path);
            Assert.Equal(expectedResultFromArchangelskToVolgograd.FinalDistance, resultFromArchangelskToVolgograd.FinalDistance);
        }
        [Fact]
        public void CheckShortestPathFromSaintPetersburgToSamara()
        {
            //Arrange
            var expectedResultFromSaintPetersburgToSamara = new ShortestPathResponseDTO
            {
                Path = new List<Guid> { saintPetersburgId, moscowId, kazanId, samaraId },
                FinalDistance = 1882
            };
            //Act
            var resultFromSaintPetersburgToSamara = testAlgorithmService.FindShortestPath(map.Id, saintPetersburgId, samaraId);
            //Assert
            Assert.Equal(expectedResultFromSaintPetersburgToSamara.Path, resultFromSaintPetersburgToSamara.Path);
            Assert.Equal(expectedResultFromSaintPetersburgToSamara.FinalDistance, resultFromSaintPetersburgToSamara.FinalDistance);
        }
        [Fact]
        public void CheckShortestPathFromEkaterinburgToVoronezh()
        {
            //Arrange
            var expectedResultFromEkaterinburgToVoronezh = new ShortestPathResponseDTO
            {
                Path = new List<Guid> { ekaterinburgId, samaraId, saratovId, voronezhId },
                FinalDistance = 1905
            };
            //Act
            var resultFromEkaterinburgToVoronezh = testAlgorithmService.FindShortestPath(map.Id, ekaterinburgId, voronezhId);
            //Assert
            Assert.Equal(expectedResultFromEkaterinburgToVoronezh.Path, resultFromEkaterinburgToVoronezh.Path);
            Assert.Equal(expectedResultFromEkaterinburgToVoronezh.FinalDistance, resultFromEkaterinburgToVoronezh.FinalDistance);
        }
    }
}
