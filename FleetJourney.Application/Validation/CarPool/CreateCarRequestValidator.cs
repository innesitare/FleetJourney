using FleetJourney.Application.Contracts.Requests.CarPool;
using FluentValidation;

namespace FleetJourney.Application.Validation.CarPool;

public sealed class CreateCarRequestValidator : AbstractValidator<CreateCarRequest>
{
    public CreateCarRequestValidator()
    {
        RuleFor(request => request.LicensePlateNumber)
            .NotEmpty().WithMessage("License plate number is required.")
            .Matches(@"^[\d\-]{1,3}-[A-Z]{2}-\d{1,2}$")
            .WithMessage("Invalid license plate number. Format should be 123-AB-45.");

        RuleFor(request => request.Brand)
            .NotEmpty()
            .WithMessage("Brand is required.");

        RuleFor(request => request.Model)
            .NotEmpty()
            .WithMessage("Model is required.");

        RuleFor(request => request.EndOfLifeMileage)
            .GreaterThan('0')
            .WithMessage("End of life mileage must be greater than zero.");

        RuleFor(request => request.MaintenanceInterval)
            .GreaterThan('0')
            .WithMessage("Maintenance interval must be greater than zero.");

        RuleFor(request => request.CurrentMileage)
            .GreaterThan('0')
            .WithMessage("Current mileage must be greater than zero.");
    }
}