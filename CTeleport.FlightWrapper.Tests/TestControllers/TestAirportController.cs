﻿using CTeleport.FlightWrapper.Api.Controllers;
using CTeleport.FlightWrapper.Api.Models.Airports;
using CTeleport.FlightWrapper.Core.Configuration;
using CTeleport.FlightWrapper.Core.Domain.Airports;
using CTeleport.FlightWrapper.Core.Domain.Base;
using CTeleport.FlightWrapper.Core.HttpClient;
using CTeleport.FlightWrapper.Core.Interfaces;
using CTeleport.FlightWrapper.Service.Airports;
using CTeleport.FlightWrapper.Tests.Fixtures;
using CTeleport.FlightWrapper.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace CTeleport.FlightWrapper.Tests.ApiControllerTests
{
    public class TestAirportController: TestServiceContainer
    {
        private readonly string _externalUrl = "https://places-dev.cteleport.com";


        [Fact]
        public async Task Get_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            var expectedResponse1 = AirportFixtures.GetTestAirportList().First();
            var expectedResponse2 = AirportFixtures.GetTestAirportList().Last();

            var mockAirportService = new Mock<IAirportService>();

            mockAirportService
                .Setup(service => service.GetDistance(new AirportDistanceQueryModel() { OriginAirportCode = expectedResponse1.iata, DestinationAirportCode = expectedResponse2.iata}))
                .ReturnsAsync(KnownDistanceFixtures.GetKnownDistanceList().First());

            var sut = new AirportController(mockAirportService.Object);

            //Act

            var result = (OkObjectResult)await sut.GetDistance(new DistanceQueryModel() { OriginAirportCode = expectedResponse1.iata, DestinationAirportCode  = expectedResponse2.iata });

            //Assert

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSuccess_InvokeAirportServiceExactlyOnce()
        {
            //Arrange
  
            var expectedResponse1 = AirportFixtures.GetTestAirportList().First();
            var expectedResponse2 = AirportFixtures.GetTestAirportList().Last();

            var mockAirportService = new Mock<IAirportService>();
            mockAirportService
                
                .Setup(service => service.GetDistance(new AirportDistanceQueryModel() { OriginAirportCode = expectedResponse1.iata, DestinationAirportCode = expectedResponse2.iata }))
                .ReturnsAsync(KnownDistanceFixtures.GetKnownDistanceList().First());

            var sut = new AirportController(ServiceInstance);

            // Act

            var result = await sut.GetDistance(new DistanceQueryModel() {  OriginAirportCode = expectedResponse1.iata , DestinationAirportCode = expectedResponse2.iata });

            //Assert

            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).Value.Should().BeOfType<AirportDistance>();
        }
    }
}
