namespace CTeleport.FlightWrapper.Api.Models.Airports
{
    public record DistanceQueryModel
    {
        public string OrginAirportCode { get; set; }

        public string DestinationAirportCode { get; set; }

        public MeasureType DistanceMeasureType { get; set; } = MeasureType.Mile;

    }

    public enum MeasureType
    { 
        Mile,
        Kilometer
    }
}
