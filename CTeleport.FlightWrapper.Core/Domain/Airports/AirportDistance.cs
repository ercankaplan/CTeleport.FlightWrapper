using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Domain.Airports
{
    public class AirportDistance
    {
        public string OrginAirportName { get; set; }
        public string OrginAirportCode { get; set; }

        public string DestinationAirportName { get; set; }
        public string DestinationAirportCode { get; set; }

        public double DistanceInMile { get; set; }
    }
}
