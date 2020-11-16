using Moq;
using Service.PathResolver;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Service;

namespace Tests
{
    public class TravelSalesmanTests
    {
        [Fact]
        public void CheckTravelSalesman5Cities()
        {
            //Arrange
            var alghorithmService = new Mock<IAlgorithmService>().Object;
            var request = new TravelSalesmanRequest()
            {
                MapId = new System.Guid("2b21b9bb-133b-4882-d3a0-28d884a52a20"),
                SelectedCities = new List<Guid>
                {
                    new Guid("2b21b9bb-133b-4882-d3a0-08d384a52a20"),
                    new Guid("2b21b9bb-133b-4882-d3a0-28d822a52a20"),
                    new Guid("2b21b9bb-133b-4882-d3a0-28d884a52a23"),
                    new Guid("2b21b9bb-133b-4882-d3a0-28e884a52a20"),
                    new Guid("2b21b9bb-133b-4882-d3a0-28e774a52a20")
                }
            };
            var expectedAns = new TravelSalesmanResponse()
            {
                PreferableSequenceOfCities = new string[] { "2b21b9bb-133b-4882-d3a0-08d384a52a20",
                    "2b21b9bb-133b-4882-d3a0-28d822a52a20",
                    "2b21b9bb-133b-4882-d3a0-28e884a52a20",
                    "2b21b9bb-133b-4882-d3a0-28d884a52a23",
                    "2b21b9bb-133b-4882-d3a0-28e774a52a20" }.Select(Guid.Parse),
                CalculatedDistance = 439,
                NameAlghorithm = nameof(TravelSalesmanAnnealingResolver)
            };
            //Act
            var actualAnswer =  alghorithmService.SolveAnnealingTravelSalesman(request).Result;
            //Assert
            Assert.Equal(expectedAns, actualAnswer);
        }
    }
}
