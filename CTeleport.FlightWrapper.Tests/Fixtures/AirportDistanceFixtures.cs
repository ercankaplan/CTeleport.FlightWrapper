using CTeleport.FlightWrapper.Core.Domain.Airports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Tests.Fixtures
{
    public static class AirportDistanceFixtures
    {
        public static List<AirportDistance> GetTestAirportDistanceList() => new()
        {
             new AirportDistance()
             {
                OrginAirportCode = "IST",
                OrginAirportName = "Istanbul Airport",
                DestinationAirportCode = "AMN",
                DestinationAirportName = "Alma",
                DistanceInMile = 5291.81,

             },
             new AirportDistance()
             {
                OrginAirportCode = "AMS",
                OrginAirportName = "Amsterdam",
                DestinationAirportCode = "SEN",
                DestinationAirportName = "Southend",
                DistanceInMile = 0,

             },
             new AirportDistance()
             {
                OrginAirportCode = "TNS",
                OrginAirportName = "Tungsten",
                DestinationAirportCode = "BER",
                DestinationAirportName = "Berlin Brandenburg",
                DistanceInMile = 0,

             }
        };

    }
}
