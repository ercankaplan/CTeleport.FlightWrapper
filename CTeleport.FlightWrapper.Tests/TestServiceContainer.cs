using CTeleport.FlightWrapper.Core.Configuration;
using CTeleport.FlightWrapper.Core.Domain.Airports;
using CTeleport.FlightWrapper.Core.HttpClient;
using CTeleport.FlightWrapper.Service.Airports;
using CTeleport.FlightWrapper.Tests.Fixtures;
using CTeleport.FlightWrapper.Tests.Helpers;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Tests
{
    public class TestServiceContainer
    {
       
        public ICTeleportHttpClient mockCTeleportHttpClient { get; set; }


        protected readonly AirportService ServiceInstance;

        public TestServiceContainer()
        {
            CreateCTeleportFakeClient();
            ServiceInstance = new AirportService(mockCTeleportHttpClient);
        }

        private void CreateCTeleportFakeClient()
        {
         
            var externalUrl = "https://places-dev.cteleport.com";
            var appSettings = new AppSettings() { HostingConfig = new HostingConfig() { AirportApiUrl = externalUrl } };

            var expectedResponse1 = AirportFixtures.GetTestAirportList().First();
            var expectedResponse2 = AirportFixtures.GetTestAirportList().Last();

            var handlerMock = MockHttpMessageHandler<Airport>.SetupHttpMockResponse(expectedResponse1, expectedResponse2, appSettings.HostingConfig.AirportApiUrl);

            var httpClient = new HttpClient(handlerMock.Object);
            var options = Options.Create(appSettings);
            mockCTeleportHttpClient = new CTeleportHttpClient(options, httpClient);

        }
    }
}
