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

namespace Tests
{
    public class ShortestPathResolverTests
    {
        
        [Fact]     
        public void CheckShortestPath()
        {
            //Arrange
            var service = new ShortestPathResolverService();
            ShortPathResolverDTO shortPathResolverDTO = new ShortPathResolverDTO();
            shortPathResolverDTO.Cities = new List<City>
            {
                new City {Name = "SaintPeterspurg", X = 90.4, Y = 285.48, Id = new Guid("9ce1a4e6-4d54-4b5e-c5c0-08d88d38ebe9")},
                new City {Name = "Moscow", X = 104.4, Y = 361.48, Id = new Guid("d68e528b-f2b3-4e34-c5c1-08d88d38ebe9")},
                new City {Name = "Smolensk", X = 58.8, Y = 357.48, Id = new Guid("6561eca6-ebbf-4da6-c5c2-08d88d38ebe9")},
                new City {Name = "Voronezh", X = 95.6, Y = 417.08, Id = new Guid("5c08ec62-a464-414e-c5c3-08d88d38ebe9")},
                new City {Name = "Saratov", X = 143.6, Y = 435.48, Id = new Guid("53c3b16c-616e-4395-c5c4-08d88d38ebe9")},
                new City {Name = "Kazan", X = 196.8, Y = 381.88, Id = new Guid("36dfc7c6-54df-4b9f-c5c5-08d88d38ebe9")},
                new City {Name = "Ekaterinburg", X = 293.6, Y = 389.08, Id = new Guid("d3a73ffc-70b4-4888-c5c6-08d88d38ebe9")},
                new City {Name = "Samara", X = 195.2, Y = 247.08, Id = new Guid("8cfe948b-912c-4e37-c5c7-08d88d38ebe9")},
                new City {Name = "Archangelsk", X = 198, Y = 247.08, Id = new Guid("d01669eb-e5dc-489e-c5c8-08d88d38ebe9")},
                new City {Name = "RostovOnDon", X = 66.4, Y = 483.26, Id = new Guid("86d39825-3c5c-4a4e-c5c9-08d88d38ebe9")},
                new City {Name = "Volgograd", X = 120, Y = 469.26, Id = new Guid("85884386-ae41-4354-c5ca-08d88d38ebe9")}
            };
            shortPathResolverDTO.Routes = new List<Route>
            {
                new Route {FirstCityId = new Guid("9ce1a4e6-4d54-4b5e-c5c0-08d88d38ebe9"), SecondCityId = new Guid("d68e528b-f2b3-4e34-c5c1-08d88d38ebe9"), Distance = 705},
                new Route {FirstCityId = new Guid("6561eca6-ebbf-4da6-c5c2-08d88d38ebe9"), SecondCityId = new Guid("d68e528b-f2b3-4e34-c5c1-08d88d38ebe9"), Distance = 398},
                new Route {FirstCityId = new Guid("5c08ec62-a464-414e-c5c3-08d88d38ebe9"), SecondCityId = new Guid("d68e528b-f2b3-4e34-c5c1-08d88d38ebe9"), Distance = 525},
                new Route {FirstCityId = new Guid("6561eca6-ebbf-4da6-c5c2-08d88d38ebe9"), SecondCityId = new Guid("5c08ec62-a464-414e-c5c3-08d88d38ebe9"), Distance = 690},
                new Route {FirstCityId = new Guid("5c08ec62-a464-414e-c5c3-08d88d38ebe9"), SecondCityId = new Guid("53c3b16c-616e-4395-c5c4-08d88d38ebe9"), Distance = 513},
                new Route {FirstCityId = new Guid("d68e528b-f2b3-4e34-c5c1-08d88d38ebe9"), SecondCityId = new Guid("36dfc7c6-54df-4b9f-c5c5-08d88d38ebe9"), Distance = 822},
                new Route {FirstCityId = new Guid("53c3b16c-616e-4395-c5c4-08d88d38ebe9"), SecondCityId = new Guid("36dfc7c6-54df-4b9f-c5c5-08d88d38ebe9"), Distance = 673},
                new Route {FirstCityId = new Guid("36dfc7c6-54df-4b9f-c5c5-08d88d38ebe9"), SecondCityId = new Guid("d3a73ffc-70b4-4888-c5c6-08d88d38ebe9"), Distance = 976},
                new Route {FirstCityId = new Guid("53c3b16c-616e-4395-c5c4-08d88d38ebe9"), SecondCityId = new Guid("8cfe948b-912c-4e37-c5c7-08d88d38ebe9"), Distance = 424},
                new Route {FirstCityId = new Guid("36dfc7c6-54df-4b9f-c5c5-08d88d38ebe9"), SecondCityId = new Guid("8cfe948b-912c-4e37-c5c7-08d88d38ebe9"), Distance = 355},
                new Route {FirstCityId = new Guid("8cfe948b-912c-4e37-c5c7-08d88d38ebe9"), SecondCityId = new Guid("d3a73ffc-70b4-4888-c5c6-08d88d38ebe9"), Distance = 968},
                new Route {FirstCityId = new Guid("9ce1a4e6-4d54-4b5e-c5c0-08d88d38ebe9"), SecondCityId = new Guid("d01669eb-e5dc-489e-c5c8-08d88d38ebe9"), Distance = 1158},
                new Route {FirstCityId = new Guid("d01669eb-e5dc-489e-c5c8-08d88d38ebe9"), SecondCityId = new Guid("d3a73ffc-70b4-4888-c5c6-08d88d38ebe9"), Distance = 1877},
                new Route {FirstCityId = new Guid("5c08ec62-a464-414e-c5c3-08d88d38ebe9"), SecondCityId = new Guid("86d39825-3c5c-4a4e-c5c9-08d88d38ebe9"), Distance = 566},
                new Route {FirstCityId = new Guid("53c3b16c-616e-4395-c5c4-08d88d38ebe9"), SecondCityId = new Guid("85884386-ae41-4354-c5ca-08d88d38ebe9"), Distance = 378},
                new Route {FirstCityId = new Guid("85884386-ae41-4354-c5ca-08d88d38ebe9"), SecondCityId = new Guid("86d39825-3c5c-4a4e-c5c9-08d88d38ebe9"), Distance = 472},
                new Route {FirstCityId = new Guid("36dfc7c6-54df-4b9f-c5c5-08d88d38ebe9"), SecondCityId = new Guid("d01669eb-e5dc-489e-c5c8-08d88d38ebe9"), Distance = 1422}
            };
            List<Guid> expectedResult = new List<Guid>
            {
                new Guid("86d39825-3c5c-4a4e-c5c9-08d88d38ebe9"), //RostovOnDon
                new Guid("5c08ec62-a464-414e-c5c3-08d88d38ebe9"), //Voronezh 
                new Guid("d68e528b-f2b3-4e34-c5c1-08d88d38ebe9") //Moscow
            };
            //Act
            //Path from Rostov to Moscow
            var guids = service.FindShortestPath(shortPathResolverDTO, "86d39825-3c5c-4a4e-c5c9-08d88d38ebe9", "d68e528b-f2b3-4e34-c5c1-08d88d38ebe9");
            foreach (var item in guids)
            {
                Console.WriteLine(item);
            }
            //Assert
            Assert.Equal(expectedResult, guids);
        }
    }
}
