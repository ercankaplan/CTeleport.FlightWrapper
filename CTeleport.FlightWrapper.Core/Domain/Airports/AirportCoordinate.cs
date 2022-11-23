using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Domain.Airports
{
    public record AirportCoordinate
    {
        [JsonPropertyName("lat")]
        public double lat { get; set; }

        [JsonPropertyName("lon")]
        public double lon { get; set; }
    }
}
