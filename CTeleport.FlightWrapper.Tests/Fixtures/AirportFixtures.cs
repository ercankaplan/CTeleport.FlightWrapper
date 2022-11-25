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
                country = "Netherlands",
                city_iata= "AMS",
                iata= "AMS",
                city= "Amsterdam",
                timezone_region_name= "Europe/Amsterdam",
                country_iata= "NL",
                rating= 3,
                name= "Amsterdam",
                location= new AirportCoordinate() {
                lon= 4.763385,
                lat= 52.309069
                },
                type= "airport",
                hubs= 7
            },
             new Airport()
            {
                country = "United States",
                city_iata= "NYC",
                iata= "LGA",
                city= "New York",
                timezone_region_name= "America/New_York",
                country_iata= "US",
                rating= 3,
                name= "LaGuardia",
                location= new AirportCoordinate() {
                lon= -73.871617,
                lat= 40.774252
                },
                type= "airport",
                hubs= 1
            },
             new Airport()
            {
                country = "Turkey",
                city_iata= "ANK",
                iata= "ESB",
                city= "Ankara",
                timezone_region_name= "Europe/Istanbul",
                country_iata= "TR",
                rating= 2,
                name= "Esenboga International",
                location= new AirportCoordinate() {
                lon= 32.993145,
                lat= 40.114941
                },
                type= "airport",
                hubs= 1
            },
             new Airport()
            {
                country = "Germany",
                city_iata= "BER",
                iata= "BER",
                city= "Berlin",
                timezone_region_name= "Europe/Berlin",
                country_iata= "DE",
                rating= 3,
                name= "Berlin Brandenburg",
                location= new AirportCoordinate() {
                lon= 13.503333,
                lat= 52.366667
                },
                type= "airport",
                hubs= 2
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
