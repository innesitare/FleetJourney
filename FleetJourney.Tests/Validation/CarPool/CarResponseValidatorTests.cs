using FleetJourney.Application.Contracts.Responses.CarPool;
using FleetJourney.Application.Validation.CarPool;
using FluentValidation.TestHelper;
using Xunit;

namespace FleetJourney.Tests.Validation.CarPool;

public sealed class CarResponseValidatorTests
{
    private readonly CarResponseValidator _validator;

    public CarResponseValidatorTests()
    {
        _validator = new CarResponseValidator();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task SetLicensePlateNumber_WithInvalidLicensePlateNumber_ShouldReturnValidationError(string licensePlateNumber)
    {
        // Arrange
        var carResponse = new CarResponse
        {
            Id = Guid.NewGuid(),
            LicensePlateNumber = licensePlateNumber,
            Brand = "Valid Brand",
            Model = "Valid Model",
            EndOfLifeMileage = 10000,
            MaintenanceInterval = 5000,
            CurrentMileage = 5000,
            Trips = null
        };

        // Act
        var result = await _validator.TestValidateAsync(carResponse);

        // Assert
        result.ShouldHaveValidationErrorFor(response => response.LicensePlateNumber)
            .WithErrorMessage("License plate number is required.");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task SetBrand_WithInvalidBrand_ShouldReturnValidationError(string brand)
    {
        // Arrange
        var carResponse = new CarResponse
        {
            Id = Guid.NewGuid(),
            LicensePlateNumber = "Valid License Plate Number",
            Brand = brand,
            Model = "Valid Model",
            EndOfLifeMileage = 10000,
            MaintenanceInterval = 5000,
            CurrentMileage = 5000,
            Trips = null
        };

        // Act
        var result = await _validator.TestValidateAsync(carResponse);

        // Assert
        result.ShouldHaveValidationErrorFor(response => response.Brand)
            .WithErrorMessage("Brand is required.");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task SetModel_WithInvalidModel_ShouldReturnValidationError(string model)
    {
        // Arrange
        var carResponse = new CarResponse
        {
            Id = Guid.NewGuid(),
            LicensePlateNumber = "Valid License Plate Number",
            Brand = "Valid Brand",
            Model = model,
            EndOfLifeMileage = 10000,
            MaintenanceInterval = 5000,
            CurrentMileage = 5000,
            Trips = null
        };

        // Act
        var result = await _validator.TestValidateAsync(carResponse);

        // Assert
        result.ShouldHaveValidationErrorFor(response => response.Model)
            .WithErrorMessage("Model is required.");
    }

    [Theory]
    [InlineData(0)]
    public async Task SetEndOfLifeMileage_WithInvalidEndOfLifeMileage_ShouldReturnValidationError(uint endOfLifeMileage)
    {
        // Arrange
        var carResponse = new CarResponse
        {
            Id = Guid.NewGuid(),
            LicensePlateNumber = "Valid License Plate Number",
            Brand = "Valid Brand",
            Model = "Valid Model",
            EndOfLifeMileage = endOfLifeMileage,
            MaintenanceInterval = 5000,
            CurrentMileage = 5000,
            Trips = null
        };

        // Act
        var result = await _validator.TestValidateAsync(carResponse);

        // Assert
        result.ShouldHaveValidationErrorFor(response => response.EndOfLifeMileage)
            .WithErrorMessage("End of life mileage must be greater than zero.");
    }

    [Theory]
    [InlineData(0)]
    public async Task SetMaintenanceInterval_WithInvalidMaintenanceInterval_ShouldReturnValidationError(uint maintenanceInterval)
    {
        // Arrange
        var carResponse = new CarResponse
        {
            Id = Guid.NewGuid(),
            LicensePlateNumber = "Valid License Plate Number",
            Brand = "Valid Brand",
            Model = "Valid Model",
            EndOfLifeMileage = 10000,
            MaintenanceInterval = maintenanceInterval,
            CurrentMileage = 5000,
            Trips = null
        };

        // Act
        var result = await _validator.TestValidateAsync(carResponse);

        // Assert
        result.ShouldHaveValidationErrorFor(response => response.MaintenanceInterval)
            .WithErrorMessage("Maintenance interval must be greater than zero.");
    }

    [Theory]
    [InlineData(0)]
    public async Task SetCurrentMileage_WithInvalidCurrentMileage_ShouldReturnValidationError(uint currentMileage)
    {
        // Arrange
        var carResponse = new CarResponse
        {
            Id = Guid.NewGuid(),
            LicensePlateNumber = "Valid License Plate Number",
            Brand = "Valid Brand",
            Model = "Valid Model",
            EndOfLifeMileage = 10000,
            MaintenanceInterval = 5000,
            CurrentMileage = currentMileage,
            Trips = null
        };

        // Act
        var result = await _validator.TestValidateAsync(carResponse);

        // Assert
        result.ShouldHaveValidationErrorFor(response => response.CurrentMileage)
            .WithErrorMessage("Current mileage must be greater than zero.");
    }

    [Fact]
    public async Task SetId_WithEmptyId_ShouldReturnValidationError()
    {
        // Arrange
        var carResponse = new CarResponse
        {
            Id = Guid.Empty,
            LicensePlateNumber = "Valid License Plate Number",
            Brand = "Valid Brand",
            Model = "Valid Model",
            EndOfLifeMileage = 10000,
            MaintenanceInterval = 5000,
            CurrentMileage = 5000,
            Trips = null
        };

        // Act
        var result = await _validator.TestValidateAsync(carResponse);

        // Assert
        result.ShouldHaveValidationErrorFor(response => response.Id)
            .WithErrorMessage("Id is required.");
    }
}