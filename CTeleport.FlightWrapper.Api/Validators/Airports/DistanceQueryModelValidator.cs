using CTeleport.FlightWrapper.Api.Models.Airports;
using FluentValidation;

namespace CTeleport.FlightWrapper.Api.Validators.Airports
{
    public class DistanceQueryModelValidator : AbstractValidator<DistanceQueryModel>
    {
        public DistanceQueryModelValidator()
        {
            RuleFor(x => x.OriginAirportCode).NotNull().WithMessage("OriginAirportCode field could not be null!")
                    .Length(3, 3).WithMessage("OriginAirportCode field must be 3 chars in length")
                    .Matches(@"^[A-Z]+").WithMessage("OriginAirportCode field must be UPPERCASE ");

            RuleFor(x => x.DestinationAirportCode).NotNull().WithMessage("DestinationAirportCode field could not be null!")
                    .Length(3, 3).WithMessage("DestinationAirportCode field must be 3 chars in length")
                    .Matches(@"^[A-Z]+$").WithMessage("DestinationAirportCode field must be UPPERCASE ");

        }
    }
}
