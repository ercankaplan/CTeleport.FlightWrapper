using CTeleport.FlightWrapper.Core.Domain.Airports;
using CTeleport.FlightWrapper.Core.Exceptions;
using CTeleport.FlightWrapper.Core.HttpClient;
using CTeleport.FlightWrapper.Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using CTeleport.FlightWrapper.Core.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Service.Airports
{
    /// <summary>
    /// we want to implement an in-memory cache for retrieving airport data. 
    /// We can directly change the ProductService class, 
    /// but that would violate the Open/Closed Principles of the SOLID Principles
    /// </summary>
    public class CachedAirportService :IAirportService
    {

        private readonly IMemoryCache _memoryCache;
        private readonly IAirportService _airportService;
        private readonly ICTeleportHttpClient _httpCTeleportClient;
        private readonly MemoryCacheEntryOptions _cacheOptions;

        public CachedAirportService(ICTeleportHttpClient httpCTeleportClient,IAirportService airportService, IMemoryCache memoryCache)
        {
            _httpCTeleportClient = httpCTeleportClient;
            _airportService = airportService;
            _memoryCache = memoryCache;
            _cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(10))
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
        }

        public async Task<Airport> GetAirport(string iataCode)
        {
            if (_memoryCache.TryGetValue(iataCode, out Airport result))
                return result;

            result = await _airportService.GetAirport(iataCode);

            _memoryCache.Set(iataCode, result, _cacheOptions);

            return result;
        }

        public async Task<AirportDistance> GetDistance(AirportDistanceQueryModel request)
        {
            var orgAirport = await this.GetAirport(request.OriginAirportCode);

            var destAirport = await this.GetAirport(request.DestinationAirportCode);

            return new AirportDistance
            {
                DestinationAirportCode = destAirport.iata,
                DestinationAirportName = destAirport.name,
                OriginAirportCode = orgAirport.iata,
                OriginAirportName = orgAirport.name,
                DistanceInMile = orgAirport.GetDistanceInMiles(destAirport)
            };
        }
    }
}
