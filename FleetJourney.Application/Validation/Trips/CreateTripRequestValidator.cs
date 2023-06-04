using FleetJourney.Application.Contracts.Requests.Trips;
using FluentValidation;

namespace FleetJourney.Application.Validation.Trips;

public sealed class CreateTripRequestValidator : AbstractValidator<CreateTripRequest>
{
    public CreateTripRequestValidator()
    {
        RuleFor(request => request.LicensePlateNumber)
            .NotEmpty()
            .WithMessage("License plate number is required.");

        RuleFor(request => request.StartMileage)
            .GreaterThan('0')
            .WithMessage("Start mileage must be greater than zero.");

        RuleFor(request => request.EndMileage)
            .GreaterThan(request => request.StartMileage)
            .WithMessage("End mileage must be greater than start mileage.");

        RuleFor(request => request.EmployeeId)
            .NotEmpty()
            .WithMessage("Employee ID is required.");
    }
}