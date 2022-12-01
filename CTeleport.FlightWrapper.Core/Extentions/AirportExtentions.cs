using CTeleport.FlightWrapper.Core.Domain.Airports;
using CTeleport.FlightWrapper.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Extentions
{
   
     public static class AirportExtentions
    {
        
        public static double GetDistanceInMiles(this Airport originAirport, Airport destinationAirport)
        {
            return DistanceCalculater.Distance(originAirport.location.lat, originAirport.location.lon, 
                destinationAirport.location.lat, destinationAirport.location.lon);
           
        }

    }
}
