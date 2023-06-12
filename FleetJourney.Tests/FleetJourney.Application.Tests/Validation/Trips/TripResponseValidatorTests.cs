using FleetJourney.Application.Contracts.Responses.Trips;
using FleetJourney.Application.Validation.Trips;
using FluentValidation.TestHelper;
using Xunit;

namespace FleetJourney.Application.Tests.Validation.Trips;

public sealed class TripResponseValidatorTests
{
    private readonly TripResponseValidator _validator;

    public TripResponseValidatorTests()
    {
        _validator = new TripResponseValidator();
    }

    [Fact]
    public async Task TripResponseValidator_ShouldHaveValidationErrorForMissingTripId()
    {
        // Arrange
        var tripResponse = new TripResponse
        {
            Id = default,
            StartMileage = 100,
            EndMileage = 200,
            EmployeeId = Guid.NewGuid(),
            CarId = Guid.NewGuid(),
            IsPrivateTrip = false
        };

        // Act
        var result = await _validator.TestValidateAsync(tripResponse);

        // Assert
        result.ShouldHaveValidationErrorFor(response => response.Id)
            .WithErrorMessage("Trip ID is required.");
    }

    [Theory]
    [InlineData(0, 100, "Start mileage must be greater than zero.")]
    public async Task TripResponseValidator_ShouldHaveValidationErrors(uint startMileage, uint endMileage, string expectedErrorMessage)
    {
        // Arrange
        var tripResponse = new TripResponse
        {
            Id = Guid.NewGuid(),
            StartMileage = startMileage,
            EndMileage = endMileage,
            EmployeeId = Guid.NewGuid(),
            CarId = Guid.NewGuid(),
            IsPrivateTrip = false
        };

        // Act
        var result = await _validator.TestValidateAsync(tripResponse);

        // Assert
        result.ShouldHaveValidationErrorFor(response => response.StartMileage)
            .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData(100, 200)]
    public async Task TripResponseValidator_ShouldNotHaveValidationErrors(uint startMileage, uint endMileage)
    {
        // Arrange
        var tripResponse = new TripResponse
        {
            Id = Guid.NewGuid(),
            StartMileage = startMileage,
            EndMileage = endMileage,
            EmployeeId = Guid.NewGuid(),
            CarId = Guid.NewGuid(),
            IsPrivateTrip = false
        };

        // Act
        var result = await _validator.TestValidateAsync(tripResponse);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task TripResponseValidator_ShouldHaveValidationErrorForMissingEmployeeId()
    {
        // Arrange
        var tripResponse = new TripResponse
        {
            Id = Guid.NewGuid(),
            StartMileage = 100,
            EndMileage = 200,
            EmployeeId = default,
            CarId = Guid.NewGuid(),
            IsPrivateTrip = false
        };

        // Act
        var result = await _validator.TestValidateAsync(tripResponse);

        // Assert
        result.ShouldHaveValidationErrorFor(response => response.EmployeeId)
            .WithErrorMessage("Employee ID is required.");
    }

    [Fact]
    public async Task TripResponseValidator_ShouldHaveValidationErrorForMissingCarId()
    {
        // Arrange
        var tripResponse = new TripResponse
        {
            Id = Guid.NewGuid(),
            StartMileage = 100,
            EndMileage = 200,
            EmployeeId = Guid.NewGuid(),
            CarId = default,
            IsPrivateTrip = false
        };

        // Act
        var result = await _validator.TestValidateAsync(tripResponse);

        // Assert
        result.ShouldHaveValidationErrorFor(response => response.CarId)
            .WithErrorMessage("Car ID is required.");
    }
}