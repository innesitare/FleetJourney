using FleetJourney.Application.Contracts.Requests.Trips;
using FleetJourney.Application.Validation.Trips;
using FluentValidation.TestHelper;
using Xunit;

namespace FleetJourney.Application.Tests.Validation.Trips;

public class UpdateTripRequestValidatorTests
{
    private readonly UpdateTripRequestValidator _validator;

    public UpdateTripRequestValidatorTests()
    {
        _validator = new UpdateTripRequestValidator();
    }

    [Theory]
    [InlineData(0, 100, "Start mileage must be greater than zero.")]
    public async Task UpdateTripRequestValidator_ShouldHaveValidationErrors(uint startMileage, uint endMileage, string expectedErrorMessage)
    {
        // Arrange
        var updateTripRequest = new UpdateTripRequest
        {
            StartMileage = startMileage,
            EndMileage = endMileage,
            EmployeeId = Guid.NewGuid(),
            CarId = Guid.NewGuid(),
            IsPrivateTrip = false
        };

        // Act
        var result = await _validator.TestValidateAsync(updateTripRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(request => request.StartMileage)
            .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData(100, 200)]
    public async Task UpdateTripRequestValidator_ShouldNotHaveValidationErrors(uint startMileage, uint endMileage)
    {
        // Arrange
        var updateTripRequest = new UpdateTripRequest
        {
            StartMileage = startMileage,
            EndMileage = endMileage,
            EmployeeId = Guid.NewGuid(),
            CarId = Guid.NewGuid(),
            IsPrivateTrip = false
        };

        // Act
        var result = await _validator.TestValidateAsync(updateTripRequest);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task UpdateTripRequestValidator_ShouldHaveValidationErrorForMissingEmployeeId()
    {
        // Arrange
        var updateTripRequest = new UpdateTripRequest
        {
            StartMileage = 100,
            EndMileage = 200,
            EmployeeId = Guid.Empty,
            CarId = Guid.NewGuid(),
            IsPrivateTrip = false
        };

        // Act
        var result = await _validator.TestValidateAsync(updateTripRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(request => request.EmployeeId)
            .WithErrorMessage("Employee ID is required.");
    }

    [Fact]
    public async Task UpdateTripRequestValidator_ShouldHaveValidationErrorForMissingCarId()
    {
        // Arrange
        var updateTripRequest = new UpdateTripRequest
        {
            StartMileage = 100,
            EndMileage = 200,
            EmployeeId = Guid.NewGuid(),
            CarId = Guid.Empty,
            IsPrivateTrip = false
        };

        // Act
        var result = await _validator.TestValidateAsync(updateTripRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(request => request.CarId)
            .WithErrorMessage("Car ID is required.");
    }
}