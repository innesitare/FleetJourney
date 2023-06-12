using FleetJourney.Application.Contracts.Requests.Trips;
using FleetJourney.Application.Validation.Trips;
using FluentValidation.TestHelper;
using Xunit;

namespace FleetJourney.Tests.Validation.Trips;

public class CreateTripRequestValidatorTests
{
    private readonly CreateTripRequestValidator _validator;

    public CreateTripRequestValidatorTests()
    {
        _validator = new CreateTripRequestValidator();
    }

    [Theory]
    [InlineData(0, 100, "Start mileage must be greater than zero.")]
    public async Task CreateTripRequestValidator_ShouldHaveValidationErrors(uint startMileage, uint endMileage, string expectedErrorMessage)
    {
        // Arrange
        var createTripRequest = new CreateTripRequest
        {
            StartMileage = startMileage,
            EndMileage = endMileage,
            EmployeeId = Guid.NewGuid(),
            CarId = Guid.NewGuid(),
            IsPrivateTrip = false
        };

        // Act
        var result = await _validator.TestValidateAsync(createTripRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(request => request.StartMileage)
            .WithErrorMessage(expectedErrorMessage);
    }

    [Theory]
    [InlineData(100, 200)]
    public async Task CreateTripRequestValidator_ShouldNotHaveValidationErrors(uint startMileage, uint endMileage)
    {
        // Arrange
        var createTripRequest = new CreateTripRequest
        {
            StartMileage = startMileage,
            EndMileage = endMileage,
            EmployeeId = Guid.NewGuid(),
            CarId = Guid.NewGuid(),
            IsPrivateTrip = false
        };

        // Act
        var result = await _validator.TestValidateAsync(createTripRequest);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task CreateTripRequestValidator_ShouldHaveValidationErrorForMissingEmployeeId()
    {
        // Arrange
        var createTripRequest = new CreateTripRequest
        {
            StartMileage = 100,
            EndMileage = 200,
            EmployeeId = Guid.Empty,
            CarId = Guid.NewGuid(),
            IsPrivateTrip = false
        };

        // Act
        var result = await _validator.TestValidateAsync(createTripRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(request => request.EmployeeId)
            .WithErrorMessage("Employee ID is required.");
    }

    [Fact]
    public async Task CreateTripRequestValidator_ShouldHaveValidationErrorForMissingCarId()
    {
        // Arrange
        var createTripRequest = new CreateTripRequest
        {
            StartMileage = 100,
            EndMileage = 200,
            EmployeeId = Guid.NewGuid(),
            CarId = Guid.Empty,
            IsPrivateTrip = false
        };

        // Act
        var result = await _validator.TestValidateAsync(createTripRequest);

        // Assert
        result.ShouldHaveValidationErrorFor(request => request.CarId)
            .WithErrorMessage("Car ID is required.");
    }
}