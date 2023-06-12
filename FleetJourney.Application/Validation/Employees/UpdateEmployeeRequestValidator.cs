using FleetJourney.Application.Contracts.Requests.Employees;
using FluentValidation;

namespace FleetJourney.Application.Validation.Employees;

public sealed class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
{
    public UpdateEmployeeRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("Name is required.");
            
        RuleFor(request => request.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.");
            
        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email address.");

        RuleFor(request => request.Birthdate)
            .NotEmpty()
            .WithMessage("Birthdate is required.");
    }
}