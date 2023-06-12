using FleetJourney.Application.Contracts.Responses.Employees;
using FleetJourney.Application.Validation.Employees;
using FluentValidation.TestHelper;
using Xunit;

namespace FleetJourney.Application.Tests.Validation.Employees;

public sealed class EmployeeResponseValidatorTests
{
    private readonly EmployeeResponseValidator _validator;

    public EmployeeResponseValidatorTests()
    {
        _validator = new EmployeeResponseValidator();
    }

    [Fact]
    public async Task EmployeeResponseValidator_ShouldHaveValidationErrorForMissingId()
    {
        // Arrange
        var employeeResponse = new EmployeeResponse
        {
            Id = Guid.Empty,
            Name = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Birthdate = DateOnly.MaxValue,
            Trips = null
        };

        // Act
        var result = await _validator.TestValidateAsync(employeeResponse);

        // Assert
        result.ShouldHaveValidationErrorFor(response => response.Id)
            .WithErrorMessage("Id is required.");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task EmployeeResponseValidator_ShouldHaveValidationErrorForInvalidName(string name)
    {
        // Arrange
        var employeeResponse = new EmployeeResponse
        {
            Id = Guid.NewGuid(),
            Name = name,
            LastName = "Doe",
            Email = "john.doe@example.com",
            Birthdate = DateOnly.MaxValue,
            Trips = null
        };

        // Act
        var result = await _validator.TestValidateAsync(employeeResponse);

        // Assert
        result.ShouldHaveValidationErrorFor(response => response.Name)
            .WithErrorMessage("Name is required.");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task EmployeeResponseValidator_ShouldHaveValidationErrorForInvalidLastName(string lastName)
    {
        // Arrange
        var employeeResponse = new EmployeeResponse
        {
            Id = Guid.NewGuid(),
            Name = "John",
            LastName = lastName,
            Email = "john.doe@example.com",
            Birthdate = DateOnly.MaxValue,
            Trips = null
        };

        // Act
        var result = await _validator.TestValidateAsync(employeeResponse);

        // Assert
        result.ShouldHaveValidationErrorFor(response => response.LastName)
            .WithErrorMessage("Last name is required.");
    }

    [Theory]
    [InlineData(null, "'Email' must not be empty.")]
    [InlineData("", "'Email' must not be empty.")]
    [InlineData("notAnEmail", "Invalid email address.")]
    public async Task EmployeeResponseValidator_ShouldHaveValidationErrorForInvalidEmail(string email,
        string expectedErrorMessage)
    {
        // Arrange
        var employeeResponse = new EmployeeResponse
        {
            Id = Guid.NewGuid(),
            Name = "John",
            LastName = "Doe",
            Email = email,
            Birthdate = DateOnly.MaxValue,
            Trips = null
        };

        // Act
        var result = await _validator.TestValidateAsync(employeeResponse);

        // Assert
        result.ShouldHaveValidationErrorFor(response => response.Email)
            .WithErrorMessage(expectedErrorMessage);
    }

    [Fact]
    public async Task EmployeeResponseValidator_ShouldHaveValidationErrorForMissingBirthdate()
    {
        // Arrange
        var employeeResponse = new EmployeeResponse
        {
            Id = Guid.NewGuid(),
            Name = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Birthdate = default(DateOnly),
            Trips = null
        };

        // Act
        var result = await _validator.TestValidateAsync(employeeResponse);

        // Assert
        result.ShouldHaveValidationErrorFor(response => response.Birthdate)
            .WithErrorMessage("Birthdate is required.");
    }

    [Fact]
    public async Task EmployeeResponseValidator_ShouldNotHaveValidationErrorForValidEmployee()
    {
        // Arrange
        var employeeResponse = new EmployeeResponse
        {
            Id = Guid.NewGuid(),
            Name = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Birthdate = DateOnly.MaxValue,
            Trips = null
        };

        // Act
        var result = await _validator.TestValidateAsync(employeeResponse);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}