using FleetJourney.Application.Contracts.Requests.Employees;
using FleetJourney.Application.Validation.Employees;
using FluentValidation.TestHelper;
using Xunit;

namespace FleetJourney.Application.Tests.Validation.Employees;

public sealed class UpdateEmployeeRequestValidatorTests
{
    private readonly UpdateEmployeeRequestValidator _validator;

    public UpdateEmployeeRequestValidatorTests()
    {
        _validator = new UpdateEmployeeRequestValidator();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task UpdateEmployeeRequestValidator_ShouldHaveValidationErrorForInvalidName(string name)
    {
        // Arrange
        var updateEmployeeRequest = new UpdateEmployeeRequest
        {
            Name = name,
            LastName = "Doe",
            Email = "john.doe@example.com",
            Birthdate = DateOnly.MaxValue
        };

        // Act
        var result = await _validator.TestValidateAsync(updateEmployeeRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(request => request.Name)
            .WithErrorMessage("Name is required.");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task UpdateEmployeeRequestValidator_ShouldHaveValidationErrorForInvalidLastName(string lastName)
    {
        // Arrange
        var updateEmployeeRequest = new UpdateEmployeeRequest
        {
            Name = "John",
            LastName = lastName,
            Email = "john.doe@example.com",
            Birthdate = DateOnly.MaxValue
        };

        // Act
        var result = await _validator.TestValidateAsync(updateEmployeeRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(request => request.LastName)
            .WithErrorMessage("Last name is required.");
    }

    [Theory]
    [InlineData(null, "'Email' must not be empty.")]
    [InlineData("", "'Email' must not be empty.")]
    [InlineData("notAnEmail", "Invalid email address.")]
    public async Task UpdateEmployeeRequestValidator_ShouldHaveValidationErrorForInvalidEmail(string email,
        string expectedErrorMessage)
    {
        // Arrange
        var updateEmployeeRequest = new UpdateEmployeeRequest
        {
            Name = "John",
            LastName = "Doe",
            Email = email,
            Birthdate = DateOnly.MaxValue
        };

        // Act
        var result = await _validator.TestValidateAsync(updateEmployeeRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(request => request.Email)
            .WithErrorMessage(expectedErrorMessage);
    }

    [Fact]
    public async Task UpdateEmployeeRequestValidator_ShouldHaveValidationErrorForMissingBirthdate()
    {
        // Arrange
        var updateEmployeeRequest = new UpdateEmployeeRequest
        {
            Name = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Birthdate = default(DateOnly)
        };

        // Act
        var result = await _validator.TestValidateAsync(updateEmployeeRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(request => request.Birthdate)
            .WithErrorMessage("Birthdate is required.");
    }

    [Fact]
    public async Task UpdateEmployeeRequestValidator_ShouldNotHaveValidationErrorForValidEmployee()
    {
        // Arrange
        var updateEmployeeRequest = new UpdateEmployeeRequest
        {
            Name = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Birthdate = DateOnly.MaxValue
        };

        // Act
        var result = await _validator.TestValidateAsync(updateEmployeeRequest);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}