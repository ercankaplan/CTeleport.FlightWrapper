﻿using CTeleport.FlightWrapper.Core.Domain.Airports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Tests.Fixtures
{
    public static class AirportDistanceFixtures
    {
        /// <summary>
        /// The shortest distance (air line) between two airports 
        /// Values are taken from https://www.distance.to/
        /// </summary>
        /// <returns></returns>
        public static List<AirportDistance> GetTestAirportDistanceList() => new()
        {
             new AirportDistance()
             {
                OrginAirportCode = "LGS",
                OrginAirportName = "LaGuardia",
                DestinationAirportCode = "AMS",
                DestinationAirportName = "Amsterdam",
                DistanceInMile = 3629.73 ,

             },
             new AirportDistance()
             {
                OrginAirportCode = "AMS",
                OrginAirportName = "Amsterdam",
                DestinationAirportCode = "IST",
                DestinationAirportName = "Istanbul Airport",
                DistanceInMile =  1367.26,

             },
             new AirportDistance()
             {
                OrginAirportCode = "ESB",
                OrginAirportName = "Esenboga International",
                DestinationAirportCode = "BER",
                DestinationAirportName = "Berlin Brandenburg",
                DistanceInMile = 1251.10,

             }
        };

    }
}
