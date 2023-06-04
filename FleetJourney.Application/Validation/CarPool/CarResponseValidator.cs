using FleetJourney.Application.Contracts.Responses.CarPool;
using FluentValidation;

namespace FleetJourney.Application.Validation.CarPool;

public sealed class CarResponseValidator : AbstractValidator<CarResponse>
{
    public CarResponseValidator()
    {
        RuleFor(response => response.LicensePlateNumber)
            .NotEmpty()
            .WithMessage("License plate number is required.");

        RuleFor(response => response.Brand)
            .NotEmpty()
            .WithMessage("Brand is required.");

        RuleFor(response => response.Model)
            .NotEmpty()
            .WithMessage("Model is required.");

        RuleFor(response => response.EndOfLifeMileage)
            .GreaterThan('0')
            .WithMessage("End of life mileage must be greater than zero.");

        RuleFor(response => response.MaintenanceInterval)
            .GreaterThan('0')
            .WithMessage("Maintenance interval must be greater than zero.");

        RuleFor(response => response.CurrentMileage)
            .GreaterThan('0')
            .WithMessage("Current mileage must be greater than zero.");
    }
}