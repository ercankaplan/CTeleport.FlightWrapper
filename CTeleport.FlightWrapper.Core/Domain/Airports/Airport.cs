using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Domain.Airports
{
    public record Airport
    {
        public string Country { get; set; }
        public string City_iata { get; set; }
        public string Iata { get; set; }
        public string City { get; set; }
        public string Timezone_Region_Name { get; set; }
        public string Country_Iata { get; set; }
        public decimal Rating { get; set; }
        public string Name { get; set; }
        public AirportCoordinate Location { get; set; }
        public string Type { get; set; }
        public int Hubs { get; set; }
    }
}
