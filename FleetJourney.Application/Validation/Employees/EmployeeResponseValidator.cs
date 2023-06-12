using FleetJourney.Application.Contracts.Responses.Employees;
using FluentValidation;

namespace FleetJourney.Application.Validation.Employees;

public sealed class EmployeeResponseValidator : AbstractValidator<EmployeeResponse>
{
    public EmployeeResponseValidator()
    {
        RuleFor(request => request.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
        
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