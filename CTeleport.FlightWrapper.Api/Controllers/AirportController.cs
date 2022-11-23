using CTeleport.FlightWrapper.Api.Models.Airports;
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

        private readonly ILogger<AirportController> _logger;

        public AirportController(ILogger<AirportController> logger, IAirportService airportService)
        {
            _logger = logger;
            _airportService = airportService;
        }

        [HttpGet(Name = "GetDistance")]
        public async Task<IActionResult> GetDistance([FromQuery] DistanceQueryModel model)
        {
           var distance = await _airportService.GetDistance(model.OrginAirportCode, model.DestinationAirportCode);

            if (distance.IsSuccess)
                return Ok(distance.Data);

            return NotFound();
        }
    }
}