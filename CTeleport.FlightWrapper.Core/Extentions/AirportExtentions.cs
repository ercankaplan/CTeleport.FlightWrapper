using CTeleport.FlightWrapper.Core.Domain.Airports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Extentions
{
   
     public static class AirportExtentions
    {
        public static double GetDistanceInMiles(this Airport orginAirport, Airport destinationAirport)
        {
            var rFromLatitude = Math.PI * orginAirport.Location.Lat / 180;
            var rToLatitude = Math.PI * destinationAirport.Location.Lat / 180;
            var theta = orginAirport.Location.Lon - destinationAirport.Location.Lon;
            var rTheta = Math.PI * theta / 180;

            var distInMiles =
                Math.Sin(rFromLatitude) * Math.Sin(rToLatitude) + Math.Cos(rFromLatitude) *
                Math.Cos(rToLatitude) * Math.Cos(rTheta);

            distInMiles = Math.Acos(distInMiles);
            distInMiles = distInMiles * 180 / Math.PI;
            distInMiles = distInMiles * 60 * 1.1515;

            return distInMiles;
        }
    }
}
