using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CTeleport.FlightWrapper.Core.Configuration;
using CTeleport.FlightWrapper.Core.Domain.Airports;
using CTeleport.FlightWrapper.Core.Domain.Base;
using CTeleport.FlightWrapper.Core.Exceptions;
using CTeleport.FlightWrapper.Core.Extentions;
using CTeleport.FlightWrapper.Core.HttpClient;
using CTeleport.FlightWrapper.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CTeleport.FlightWrapper.Service.Airports
{
    public class AirportService : IAirportService
    {

        private readonly ICTeleportHttpClient _httpCTeleportClient;

        /// <summary>
        /// Constractor of AirportService
        /// </summary>
        /// <param name="httpCTeleportClient">passes the instance of ICTeleportHttpClient with dependency injection</param>
        public AirportService(ICTeleportHttpClient httpCTeleportClient)
        {
            _httpCTeleportClient = httpCTeleportClient;
        }

        /// <summary>
        /// Gets airport detail based on given  iataCode
        /// </summary>
        /// <param name="iataCode"></param>
        /// <returns></returns>
        /// <exception cref="AirportNotFoundException"></exception>
        public async Task<Airport> GetAirport(string iataCode)
        {
            var airportResponse = await _httpCTeleportClient.GetAsync<Airport>(string.Format(AirportServiceDefaults.ApiGet_AirportByIATACode, iataCode));

            if (airportResponse.IsSuccess)
                return airportResponse.Data;

            throw new AirportNotFoundException($"Airport not found for {iataCode}");


        }

        /// <summary>
        /// Gets the shortest distance between two airport location
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="AirportNotFoundException"></exception>
        public async Task<AirportDistance> GetDistance(AirportDistanceQueryModel request)
        {
            var originAirport = await this.GetAirport(request.OriginAirportCode);

            var destAirport = await this.GetAirport(request.DestinationAirportCode);

            return new AirportDistance
            {
                    DestinationAirportCode = destAirport.iata,
                    DestinationAirportName = destAirport.name,
                    OriginAirportCode = originAirport.iata,
                    OriginAirportName = originAirport.name,
                    DistanceInMile = originAirport.GetDistanceInMiles(destAirport)
            };

        }
    }
}
