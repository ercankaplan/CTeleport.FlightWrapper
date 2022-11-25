using CTeleport.FlightWrapper.Core.Domain.Airports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTeleport.FlightWrapper.Tests.Fixtures
{
    public static class AirportFixtures
    {
        public static List<Airport> GetTestAirportList() => new()
        {
            new Airport()
            {
                country = "United States",
                city_iata= "AMN",
                iata= "AMN",
                city= "Alma",
                timezone_region_name= "America/New_York",
                country_iata= "US",
                rating= 0,
                name= "Alma",
                location= new AirportCoordinate() {
                lon= -84.65,
                lat= 43.383333
                },
                type= "airport",
                hubs= 0
            },
            new Airport()
            {
                country = "Turkey",
                city_iata= "IST",
                iata= "IST",
                city= "Istanbul",
                timezone_region_name= "Europe/Istanbul",
                country_iata= "TR",
                rating= 3,
                name= "Istanbul Airport",
                location= new AirportCoordinate() {
                lon= 28.815278,
                lat= 40.976667
                },
                type= "airport",
                hubs= 5
            }
        };
    }
}
