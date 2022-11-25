using CTeleport.FlightWrapper.Core.Domain.Airports;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Tests.Helpers
{


    public static class MockHttpMessageHandler<T>
    {
        internal static Mock<HttpMessageHandler> SetupBasicGetResourceList<T>(T expectedResponse)
        {
            var mockResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
            };

            mockResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpResponseMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse);

            return handlerMock;
        }

        internal static Mock<HttpMessageHandler> SetupHttpMockResponse(Airport expectedResponse1, Airport expectedResponse2, string endpoint)
        {
            var mockResponse1 = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse1))
            };

            mockResponse1.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");


            var httpRequestMessage1 = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{endpoint}/airports/{expectedResponse1.iata}"),
                Method = HttpMethod.Get,
            };

            var mockResponse2 = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse2))
            };

            mockResponse2.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");


            var httpRequestMessage2 = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{endpoint}/airports/{expectedResponse2.iata}"),
                Method = HttpMethod.Get,
            };

            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(x => x.RequestUri != httpRequestMessage1.RequestUri), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
            });

            handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(x => x.RequestUri != httpRequestMessage2.RequestUri), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
            });

            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == httpRequestMessage1.RequestUri), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse1);

            handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == httpRequestMessage2.RequestUri), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse2);

          

            


            return handlerMock;
        }
    }
}
