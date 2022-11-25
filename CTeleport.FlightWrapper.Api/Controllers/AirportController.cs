using CTeleport.FlightWrapper.Api.Models.Airports;
using CTeleport.FlightWrapper.Core.Domain.Airports;
using CTeleport.FlightWrapper.Core.Interfaces;
using CTeleport.FlightWrapper.Service.Airports;
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

        [HttpGet("GetDistance")]
        public async Task<IActionResult> GetDistance([FromQuery] DistanceQueryModel model)
        {
            //Todo auto mapper
            AirportDistanceQueryModel request = new AirportDistanceQueryModel()
            {
                DestinationAirportCode = model.DestinationAirportCode,
                OrginAirportCode = model.OrginAirportCode
            };

            var distance = await _airportService.GetDistance(request);

            return Ok(distance);


        }
    }
}