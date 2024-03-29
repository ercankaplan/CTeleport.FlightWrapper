﻿using CTeleport.FlightWrapper.Core.Domain.Airports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Tests.Fixtures
{
    public static class KnownDistanceFixtures
    {
        /// <summary>
        /// The shortest distance (air line) between two airports 
        /// Values are taken from https://www.distance.to/
        /// </summary>
        /// <returns></returns>
        public static List<AirportDistance> GetKnownDistanceList() => new()
        {
             new AirportDistance()
             {
                OriginAirportCode = "LGS",
                OriginAirportName = "LaGuardia",
                DestinationAirportCode = "AMS",
                DestinationAirportName = "Amsterdam",
                DistanceInMile = 3629.73 ,

             },
             new AirportDistance()
             {
                OriginAirportCode = "AMS",
                OriginAirportName = "Amsterdam",
                DestinationAirportCode = "IST",
                DestinationAirportName = "Istanbul Airport",
                DistanceInMile =  1367.26,

             },
             new AirportDistance()
             {
                OriginAirportCode = "ESB",
                OriginAirportName = "Esenboga International",
                DestinationAirportCode = "BER",
                DestinationAirportName = "Berlin Brandenburg",
                DistanceInMile = 1251.10,

             },

        };

        public static AirportDistance GetKnownDistance(string orgCode, string desCode)
        {
           var response = GetKnownDistanceList()
                .Where(x => x.OriginAirportCode == orgCode && x.DestinationAirportCode == desCode)
                .SingleOrDefault();

            return response is null ? new AirportDistance() : response;
        }

    }
}
