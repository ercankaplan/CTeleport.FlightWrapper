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
        /// <summary>
        /// This uses the ‘haversine’ formula to calculate the great-circle distance between two points – that is, 
        /// the shortest distance over the earth’s surface – giving an ‘as-the-crow-flies’ distance between the points 
        /// </summary>
        /// <param name="orginAirport"></param>
        /// <param name="destinationAirport"></param>
        /// <returns></returns>
        public static double GetDistanceInMiles(this Airport orginAirport, Airport destinationAirport)
        {

            var R = 6371; // Radius of the earth in km
            var dLat = (destinationAirport.location.lat - orginAirport.location.lat) * (Math.PI / 180);  
            var dLon = (destinationAirport.location.lon - orginAirport.location.lon) * (Math.PI / 180);
            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(orginAirport.location.lat * (Math.PI / 180)) * Math.Cos(destinationAirport.location.lat * (Math.PI / 180)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
              ;
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d / 1.60934;
        }

        
       
    }
}
