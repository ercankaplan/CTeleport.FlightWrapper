using CTeleport.FlightWrapper.Api.Models.Airports;
using FluentValidation;

namespace CTeleport.FlightWrapper.Api.Validators.Airports
{
    public class DistanceQueryModelValidator : AbstractValidator<DistanceQueryModel>
    {
        public DistanceQueryModelValidator()
        {
            RuleFor(x => x.OrginAirportCode).NotNull().WithMessage("OrginAirportCode field could not be null!")
                    .Length(3, 3).WithMessage("OrginAirportCode field must be 3 chars in length")
                    .Matches(@"^[A-Z]+").WithMessage("OrginAirportCode field must be UPPERCASE ");

            RuleFor(x => x.DestinationAirportCode).NotNull().WithMessage("DestinationAirportCode field could not be null!")
                    .Length(3, 3).WithMessage("DestinationAirportCode field must be 3 chars in length")
                    .Matches(@"^[A-Z]+$").WithMessage("DestinationAirportCode field must be UPPERCASE ");

        }
    }
}
