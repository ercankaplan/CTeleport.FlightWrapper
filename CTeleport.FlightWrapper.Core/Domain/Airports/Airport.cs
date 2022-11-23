using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Domain.Airports
{
    public record Airport
    {
        [JsonPropertyName("country")]
        public string country { get; set; }

        [JsonPropertyName("city_iata")]
        public string city_iata { get; set; }

        [JsonPropertyName("iata")]
        public string iata { get; set; }

        [JsonPropertyName("city")]
        public string city { get; set; }

        [JsonPropertyName("timezone_region_name")]
        public string timezone_region_name { get; set; }

        [JsonPropertyName("country_iata")]
        public string country_iata { get; set; }

        [JsonPropertyName("rating")]
        public decimal rating { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("location")]
        public AirportCoordinate location { get; set; }

        [JsonPropertyName("type")]
        public string type { get; set; }

        [JsonPropertyName("hubs")]
        public int hubs { get; set; }
    }
}
