using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Core.Helpers
{
    public static class DistanceCalculater
    {

        /// <summary>
        /// calculates the distance between two points (given the latitude/longitude of those points).
        /// South latitudes are negative, east longitudes are positive
        /// </summary>
        /// <param name="lat1">Latitude of point 1 (in decimal degrees)</param>
        /// <param name="lon1">Longitude of point 1 (in decimal degrees)</param>
        /// <param name="lat2">Latitude of point 2 (in decimal degrees)</param>
        /// <param name="lon2">Longitude of point 2 (in decimal degrees)</param>
        /// <param name="unit">unit = the unit you desire for results 
        /// where: 'M' is statute miles (default)
        /// 'K' is kilometers  
        /// 'N' is nautical miles</param>
        /// <returns></returns>
        public static double Distance(double lat1, double lon1, double lat2, double lon2, char unit ='M')
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == 'K')
                {
                    dist = dist * 1.609344;
                }
                else if (unit == 'N')
                {
                    dist = dist * 0.8684;
                }
                return (dist);
            }
        }

        /// <summary>
        /// This function converts decimal degrees to radians 
        /// </summary>
        /// <param name="deg"></param>
        /// <returns></returns>
        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        /// <summary>
        /// This function converts radians to decimal degrees
        /// </summary>
        /// <param name="rad"></param>
        /// <returns></returns>
        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
