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
        public AirportService(ICTeleportHttpClient httpCTeleportClient)
        {
            _httpCTeleportClient = httpCTeleportClient;
        }

        public async Task<Airport> GetAirport(string iataCode)
        {
            var airportResponse = await _httpCTeleportClient.GetAsync<Airport>(string.Format(AirportServiceDefaults.ApiGet_AirportByIATACode, iataCode));

            if (airportResponse.IsSuccess)
                return airportResponse.Data;

            throw new AirportNotFoundException("Airport not found");


        }

        public async Task<AirportDistance> GetDistance(AirportDistanceQueryModel request)
        {
            var orgAirportResponse = await _httpCTeleportClient.GetAsync<Airport>(string.Format(AirportServiceDefaults.ApiGet_AirportByIATACode, request.OrginAirportCode));

            if (!orgAirportResponse.IsSuccess)
            {
                throw new AirportNotFoundException("Orgin Airport not found");
            }
            var destAirportResponse = await _httpCTeleportClient.GetAsync<Airport>(string.Format(AirportServiceDefaults.ApiGet_AirportByIATACode, request.DestinationAirportCode));

            if (!destAirportResponse.IsSuccess)
            {
                throw new AirportNotFoundException("Destination Airport not found");
            }

            return new AirportDistance
            {
                    DestinationAirportCode = destAirportResponse.Data.iata,
                    DestinationAirportName = destAirportResponse.Data.name,
                    OrginAirportCode = orgAirportResponse.Data.iata,
                    OrginAirportName = orgAirportResponse.Data.name,
                    DistanceInMile = orgAirportResponse.Data.GetDistanceInMiles(destAirportResponse.Data)
            };

        }
    }
}
