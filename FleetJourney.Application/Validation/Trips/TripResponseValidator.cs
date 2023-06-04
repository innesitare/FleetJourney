using FleetJourney.Application.Contracts.Responses.Trips;
using FluentValidation;

namespace FleetJourney.Application.Validation.Trips;

public sealed class TripResponseValidator : AbstractValidator<TripResponse>
{
    public TripResponseValidator()
    {
        RuleFor(response => response.Id)
            .NotEmpty()
            .WithMessage("Trip ID is required.");

        RuleFor(response => response.LicensePlateNumber)
            .NotEmpty()
            .WithMessage("License plate number is required.");

        RuleFor(response => response.StartMileage)
            .GreaterThan('0')
            .WithMessage("Start mileage must be greater than zero.");

        RuleFor(response => response.EndMileage)
            .GreaterThan(response => response.StartMileage)
            .WithMessage("End mileage must be greater than start mileage.");

        RuleFor(response => response.EmployeeId)
            .NotEmpty()
            .WithMessage("Employee ID is required.");
    }
}