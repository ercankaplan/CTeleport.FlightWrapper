namespace CTeleport.FlightWrapper.Api.Models.Airports
{
    /// <summary>
    /// Model that will bequering di
    /// </summary>
    public record DistanceQueryModel
    {
        /// <summary>
        /// The IATA aiport code of origin airport 
        /// </summary>
        public string OriginAirportCode { get; set; }

        /// <summary>
        /// The IATA aiport code of destination airport
        /// </summary>
        public string DestinationAirportCode { get; set; }


    }

   
}
