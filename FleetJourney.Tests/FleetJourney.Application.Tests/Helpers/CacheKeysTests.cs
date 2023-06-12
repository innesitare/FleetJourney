using FleetJourney.Application.Helpers;
using FluentAssertions;
using Xunit;

namespace FleetJourney.Application.Tests.Helpers;

public sealed class CacheKeysTests
{
    [Theory]
    [InlineData("employees-all")]
    public void Employees_GetAll_Should_Return_Correct_Key(string expectedKey)
    {
        // Act
        var key = CacheKeys.Employees.GetAll;

        // Assert
        key.Should().Be(expectedKey);
    }

    [Theory]
    [InlineData("employees-", "")]
    public void Employees_Get_Should_Return_Correct_Key(string prefix, string suffix)
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var expectedKey = $"{prefix}{employeeId}{suffix}";

        // Act
        var key = CacheKeys.Employees.Get(employeeId);

        // Assert
        key.Should().Be(expectedKey);
    }

    [Theory]
    [InlineData("employees-email-", "test@example.com")]
    public void Employees_GetByEmail_Should_Return_Correct_Key(string prefix, string email)
    {
        // Arrange
        var expectedKey = $"{prefix}{email}";

        // Act
        var key = CacheKeys.Employees.GetByEmail(email);

        // Assert
        key.Should().Be(expectedKey);
    }

    [Theory]
    [InlineData("cars-all")]
    public void CarPool_GetAll_Should_Return_Correct_Key(string expectedKey)
    {
        // Act
        var key = CacheKeys.CarPool.GetAll;

        // Assert
        key.Should().Be(expectedKey);
    }

    [Theory]
    [InlineData("cars-", "")]
    public void CarPool_Get_Should_Return_Correct_Key(string prefix, string suffix)
    {
        // Arrange
        var carId = Guid.NewGuid();
        var expectedKey = $"{prefix}{carId}{suffix}";

        // Act
        var key = CacheKeys.CarPool.Get(carId);

        // Assert
        key.Should().Be(expectedKey);
    }

    [Theory]
    [InlineData("trips-all")]
    public void Trips_GetAll_Should_Return_Correct_Key(string expectedKey)
    {
        // Act
        var key = CacheKeys.Trips.GetAll;

        // Assert
        key.Should().Be(expectedKey);
    }

    [Theory]
    [InlineData("trips-", "-all")]
    public void Trips_GetAllByEmployeeId_Should_Return_Correct_Key(string prefix, string suffix)
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var expectedKey = $"{prefix}{employeeId}{suffix}";

        // Act
        var key = CacheKeys.Trips.GetAllByEmployeeId(employeeId);

        // Assert
        key.Should().Be(expectedKey);
    }

    [Theory]
    [InlineData("trips-", "")]
    public void Trips_Get_Should_Return_Correct_Key(string prefix, string suffix)
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var expectedKey = $"{prefix}{tripId}{suffix}";

        // Act
        var key = CacheKeys.Trips.Get(tripId);

        // Assert
        key.Should().Be(expectedKey);
    }

    [Theory]
    [InlineData("trips-", "")]
    public void Trips_GetByCarId_Should_Return_Correct_Key(string prefix, string suffix)
    {
        // Arrange
        var carId = Guid.NewGuid();
        var expectedKey = $"{prefix}{carId}{suffix}";

        // Act
        var key = CacheKeys.Trips.GetByCarId(carId);

        // Assert
        key.Should().Be(expectedKey);
    }
}