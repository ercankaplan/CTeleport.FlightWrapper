using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Domain.Airports
{
    public class AirportDistanceQueryModel
    {
        public string OriginAirportCode { get; set; }
        public string DestinationAirportCode { get; set; }
    }
}
