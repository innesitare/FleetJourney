using FleetJourney.Application.Contracts.Requests.Trips;
using FleetJourney.Application.Contracts.Responses.Trips;
using FleetJourney.Application.Mapping;
using FleetJourney.Domain.Trips;
using Xunit;

namespace FleetJourney.Application.Tests.Mapping;

public sealed class TripMapperTests
{
    [Fact]
    public void ToResponse_Should_Map_Trip_To_TripResponse()
    {
        // Arrange
        var trip = new Trip
        {
            StartMileage = 1000,
            EndMileage = 2000,
            IsPrivateTrip = false,
            EmployeeId = Guid.NewGuid(),
            CarId = Guid.NewGuid()
        };

        // Act
        var result = trip.ToResponse();

        // Assert
        Assert.IsType<TripResponse>(result);
    }

    [Fact]
    public void ToTrip_Should_Map_CreateTripRequest_To_Trip()
    {
        // Arrange
        var request = new CreateTripRequest
        {
            StartMileage = 1000,
            EndMileage = 2000,
            IsPrivateTrip = false,
            EmployeeId = Guid.NewGuid(),
            CarId = Guid.NewGuid()
        };

        // Act
        var result = request.ToTrip();

        // Assert
        Assert.IsType<Trip>(result);
    }

    [Fact]
    public void ToTrip_Should_Map_UpdateTripRequest_To_Trip()
    {
        // Arrange
        var request = new UpdateTripRequest
        {
            StartMileage = 1000,
            EndMileage = 2000,
            IsPrivateTrip = false,
            EmployeeId = Guid.NewGuid(),
            CarId = Guid.NewGuid()
        };

        // Act
        var result = request.ToTrip();

        // Assert
        Assert.IsType<Trip>(result);
    }

    [Fact]
    public void ToTrip_Should_Set_Id_In_UpdateTripRequest_And_Map_To_Trip()
    {
        // Arrange
        var request = new UpdateTripRequest
        {
            StartMileage = 1000,
            EndMileage = 2000,
            IsPrivateTrip = false,
            EmployeeId = Guid.NewGuid(),
            CarId = Guid.NewGuid()
        };
        var id = Guid.NewGuid();

        // Act
        var result = request.ToTrip(id);

        // Assert
        Assert.Equal(id, request.Id);
        Assert.IsType<Trip>(result);
    }
}