using CTeleport.FlightWrapper.Core.Configuration;
using CTeleport.FlightWrapper.Core.Domain.Airports;
using CTeleport.FlightWrapper.Core.Domain.Base;
using CTeleport.FlightWrapper.Core.HttpClient;
using CTeleport.FlightWrapper.Core.Interfaces;
using CTeleport.FlightWrapper.Service.Airports;
using CTeleport.FlightWrapper.Tests.Fixtures;
using CTeleport.FlightWrapper.Tests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using FluentAssertions.Execution;
using CTeleport.FlightWrapper.Core.Exceptions;

namespace CTeleport.FlightWrapper.Tests.ServiceTests
{

    public class TestAirportService
    {
        private readonly string   _externalUrl = "https://places-dev.cteleport.com";


        [Fact]
        public async Task Should_Return_The_Distance_With_A_Tolerance_Lower_Than_0dot5()
        {
            // Arrange

            var appSettings = new AppSettings() { HostingConfig = new HostingConfig() { AirportApiUrl = _externalUrl } };
            var expectedResponse1 = AirportFixtures.GetTestAirportList().First();
            var expectedResponse2 = AirportFixtures.GetTestAirportList().Last();
            var handlerMock = MockHttpMessageHandler<Airport>.SetupHttpMockResponse(expectedResponse1, expectedResponse2, appSettings.HostingConfig.AirportApiUrl);

            var httpClient = new HttpClient(handlerMock.Object);
            var options = Options.Create(appSettings);
            var mockCTeleportHttpClient = new CTeleportHttpClient(options, httpClient);

            var sut = new AirportService(mockCTeleportHttpClient);

            // Act

            var result = await sut.GetDistance(new AirportDistanceQueryModel() { OriginAirportCode = expectedResponse1.iata, DestinationAirportCode = expectedResponse2.iata });

            // Assert
            Assert.NotNull(result);
            AirportDistance knownDistance = KnownDistanceFixtures.GetKnownDistance(expectedResponse1.iata, expectedResponse2.iata);
            var tolerance = Math.Abs(result.DistanceInMile -  knownDistance.DistanceInMile) * 100 / knownDistance.DistanceInMile;
            Assert.True(tolerance < 0.5);
        }

        [Fact]
        public async Task Should_Return_AirportNotFoundException_When_Called_Invalid_Airport_Code()
        {
            // Arrange

            var appSettings = new AppSettings() { HostingConfig = new HostingConfig() { AirportApiUrl = _externalUrl } };

            var expectedResponse1 = AirportFixtures.GetTestAirportList().First();
            var expectedResponse2 = AirportFixtures.GetTestAirportList().Last();

            var handlerMock = MockHttpMessageHandler<Airport>.SetupHttpMockResponse(expectedResponse1, expectedResponse2, appSettings.HostingConfig.AirportApiUrl);

            var httpClient = new HttpClient(handlerMock.Object);
            var options = Options.Create(appSettings);
            var mockCTeleportHttpClient = new CTeleportHttpClient(options, httpClient);

            var sut = new AirportService(mockCTeleportHttpClient);

            // Act

            async Task Act() => await sut.GetDistance(new AirportDistanceQueryModel() { OriginAirportCode = "XXX", DestinationAirportCode = expectedResponse2.iata });

            // Assert
            await Assert.ThrowsAsync<AirportNotFoundException>(Act);
        }

            [Fact]
        public async void Should_Return_Airport()
        {
            //Arrange

            var expectedResponse = AirportFixtures.GetTestAirportList().First();
            var iataCode = expectedResponse.iata;

            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse)),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            var httpClient = new HttpClient(handlerMock.Object);
            var _appSettings = new AppSettings() { HostingConfig = new HostingConfig() { AirportApiUrl = _externalUrl } };
            var options = Options.Create(_appSettings);
            var httpCTeleportClient = new CTeleportHttpClient(options,httpClient);

            var sut = new AirportService(httpCTeleportClient);

            // Act

            var result = await sut.GetAirport(iataCode);

            // Assert

            Assert.NotNull(result);
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>());
        }


        [Fact]
        public async void ShouldReturn_AMS_Airport_When_Invokes_With_Code_AMS()
        {

            

            var expectedResponse = AirportFixtures.GetTestAirportList().Where(x=> x.iata=="AMS").Single();

            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse)),
            };



            var httpRequestMessage = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{_externalUrl}/airports/{expectedResponse.iata}"),
                Method = HttpMethod.Get,
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.Is<HttpRequestMessage>(x=> x.RequestUri == httpRequestMessage.RequestUri),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response)
               .Verifiable();


            var httpClient = new HttpClient(handlerMock.Object);
            var _appSettings = new AppSettings() { HostingConfig = new HostingConfig() { AirportApiUrl = _externalUrl } };
            var options = Options.Create(_appSettings);
            var httpCTeleportClient = new CTeleportHttpClient(options, httpClient);


            var sut = new AirportService(httpCTeleportClient);

            var result = await sut.GetAirport("AMS");

            Assert.NotNull(result);
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == httpRequestMessage.RequestUri),
               ItExpr.IsAny<CancellationToken>());
        }

       

        

        //[Fact]
        //public async void GetDistance_WhenCalled_InvokesHttpGetRequest()
        //{
        //    //arrange
        //    var expectedResponse = AirportDistanceFixtures.GetTestAirportDistanceList().First();
        //    var handlerMock = MockHttpMessageHandler<AirportDistance>.SetupBasicGetResourceList(expectedResponse);

        //    var httpClient = new HttpClient(handlerMock.Object);
        //    var options = Options.Create(new AppSettings() { HostingConfig = new HostingConfig() { AirportApiUrl = "https://places-dev.cteleport.com" } });
        //    var httpCTeleportClient = new CTeleportHttpClient(options, httpClient);
        //    var sut = new AirportService(httpCTeleportClient);

        //    //Act
        //    await sut.GetDistance(new AirportDistanceQueryModel() { OrginAirportCode = expectedResponse.OrginAirportCode, DestinationAirportCode = expectedResponse.DestinationAirportCode});

        //    //Assert
        //    handlerMock.Protected().Verify(
        //       "SendAsyn", 
        //       Times.Exactly(2),
        //       ItExpr.Is<HttpRequestMessage>(req=> req.Method== HttpMethod.Get),
        //       ItExpr.IsAny<CancellationToken>()
        //       );

        //}


        //[Fact]
        //public async void GetDistance_WhenCalled_InvokesExternalUrl()
        //{
        //    //arrange


        //    var externalUrl = "https://places-dev.cteleport.com";
        //    var appSettings = new AppSettings() { HostingConfig = new HostingConfig() { AirportApiUrl = externalUrl } };

        //    var expectedResponse1 = AirportFixtures.GetTestAirportList().First();
        //    var expectedResponse2 = AirportFixtures.GetTestAirportList().Last();

        //    var handlerMock = HttpClientHelper<Airport>.SetupHttpMockResponse(expectedResponse1,expectedResponse2, appSettings.HostingConfig.AirportApiUrl);

        //    var httpClient = new HttpClient(handlerMock.Object);
        //    var options = Options.Create(appSettings);
        //    var httpCTeleportClient = new CTeleportHttpClient(options, httpClient);


        //    var sut = new AirportService(httpCTeleportClient);


        //    //Act
        //    await sut.GetDistance(new AirportDistanceQueryModel() { OrginAirportCode = expectedResponse1.iata, DestinationAirportCode = expectedResponse2.iata });

        //    //Assert
        //    handlerMock.Protected().Verify(
        //       "SendAsyn",
        //       Times.Exactly(1),
        //       ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.AbsoluteUri == externalUrl),
        //       ItExpr.IsAny<CancellationToken>()
        //       );

        //}
    }
}
