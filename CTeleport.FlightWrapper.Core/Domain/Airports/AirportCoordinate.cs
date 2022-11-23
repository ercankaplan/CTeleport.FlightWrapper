using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Domain.Airports
{
    public record AirportCoordinate
    {
        public double Lat { get; set; }

        public double Lon { get; set; }
    }
}
