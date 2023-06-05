﻿using FleetJourney.Application.Contracts.Requests.Employees;
using FluentValidation;

namespace FleetJourney.Application.Validation.EmployeesValidation;

public sealed class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
{
    public CreateEmployeeRequestValidator()
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

