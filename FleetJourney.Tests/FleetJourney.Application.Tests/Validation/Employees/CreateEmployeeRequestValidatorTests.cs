using FleetJourney.Application.Contracts.Requests.Employees;
using FleetJourney.Application.Validation.Employees;
using FluentValidation.TestHelper;
using Xunit;

namespace FleetJourney.Application.Tests.Validation.Employees;

public sealed class CreateEmployeeRequestValidatorTests
{
    private readonly CreateEmployeeRequestValidator _validator;

    public CreateEmployeeRequestValidatorTests()
    {
        _validator = new CreateEmployeeRequestValidator();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateEmployeeRequestValidator_ShouldHaveValidationErrorForInvalidName(string name)
    {
        // Arrange
        var createEmployeeRequest = new CreateEmployeeRequest
        {
            Name = name,
            LastName = "Doe",
            Email = "john.doe@example.com",
            Birthdate = DateOnly.MaxValue
        };

        // Act
        var result = await _validator.TestValidateAsync(createEmployeeRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(request => request.Name)
            .WithErrorMessage("Name is required.");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateEmployeeRequestValidator_ShouldHaveValidationErrorForInvalidLastName(string lastName)
    {
        // Arrange
        var createEmployeeRequest = new CreateEmployeeRequest
        {
            Name = "John",
            LastName = lastName,
            Email = "john.doe@example.com",
            Birthdate = DateOnly.MaxValue
        };

        // Act
        var result = await _validator.TestValidateAsync(createEmployeeRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(request => request.LastName)
            .WithErrorMessage("Last name is required.");
    }

    [Theory]
    [InlineData(null, "'Email' must not be empty.")]
    [InlineData("", "'Email' must not be empty.")]
    [InlineData("notAnEmail", "Invalid email address.")]
    public async Task CreateEmployeeRequestValidator_ShouldHaveValidationErrorForInvalidEmail(string email,
        string expectedErrorMessage)
    {
        // Arrange
        var createEmployeeRequest = new CreateEmployeeRequest
        {
            Name = "John",
            LastName = "Doe",
            Email = email,
            Birthdate = DateOnly.MaxValue
        };

        // Act
        var result = await _validator.TestValidateAsync(createEmployeeRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(request => request.Email)
            .WithErrorMessage(expectedErrorMessage);
    }

    [Fact]
    public async Task CreateEmployeeRequestValidator_ShouldHaveValidationErrorForMissingBirthdate()
    {
        // Arrange
        var createEmployeeRequest = new CreateEmployeeRequest
        {
            Name = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Birthdate = default(DateOnly)
        };

        // Act
        var result = await _validator.TestValidateAsync(createEmployeeRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(request => request.Birthdate)
            .WithErrorMessage("Birthdate is required.");
    }

    [Fact]
    public async Task CreateEmployeeRequestValidator_ShouldNotHaveValidationErrorForValidEmployee()
    {
        // Arrange
        var createEmployeeRequest = new CreateEmployeeRequest
        {
            Name = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Birthdate = DateOnly.MaxValue
        };

        // Act
        var result = await _validator.TestValidateAsync(createEmployeeRequest);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}