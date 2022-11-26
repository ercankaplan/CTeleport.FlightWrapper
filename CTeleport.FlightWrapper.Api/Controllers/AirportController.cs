using CTeleport.FlightWrapper.Api.Models.Airports;
using CTeleport.FlightWrapper.Core.Domain.Airports;
using CTeleport.FlightWrapper.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CTeleport.FlightWrapper.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirportController : ControllerBase
    {
        private readonly IAirportService _airportService;

        public AirportController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        /// <summary>
        /// Gets airport details that is provided by https://places-dev.cteleport.com
        /// </summary>
        /// <param name="airportCode">IATA airport code</param>
        /// <returns></returns>
        [HttpGet("Airport")]
        public async Task<IActionResult> GetAirport([FromQuery] string airportCode)
        {
            var airport = await _airportService.GetAirport(airportCode);

            return Ok(airport);
        }

        /// <summary>
        /// Gets the shortest distance between two airport location based on given iata airport code
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("GetDistance")]
        public async Task<IActionResult> GetDistance([FromQuery] DistanceQueryModel model)
        {
            //Todo auto mapper
            AirportDistanceQueryModel request = new AirportDistanceQueryModel()
            {
                DestinationAirportCode = model.DestinationAirportCode,
                OriginAirportCode = model.OriginAirportCode
            };

            var distance = await _airportService.GetDistance(request);

            return Ok(distance);


        }
    }
}