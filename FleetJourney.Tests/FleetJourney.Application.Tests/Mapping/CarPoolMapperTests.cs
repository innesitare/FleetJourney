using FleetJourney.Application.Contracts.Requests.CarPool;
using FleetJourney.Application.Contracts.Responses.CarPool;
using FleetJourney.Application.Mapping;
using FleetJourney.Domain.CarPool;
using Xunit;

namespace FleetJourney.Application.Tests.Mapping
{
    public sealed class CarPoolMapperTests
    {
        [Fact]
        public void ToResponse_Should_Map_Car_To_CarResponse()
        {
            // Arrange
            var car = new Car
            {
                LicensePlateNumber = "123-AB-45",
                Brand = "Ford",
                Model = "Mustang",
                EndOfLifeMileage = 24000,
                MaintenanceInterval = 5000,
                CurrentMileage = 12000
            };


            // Act
            var result = car.ToResponse();

            // Assert
            Assert.IsType<CarResponse>(result);
        }

        [Fact]
        public void ToCar_Should_Map_CreateCarRequest_To_Car()
        {
            // Arrange
            var request = new CreateCarRequest
            {
                LicensePlateNumber = "123-AB-45",
                Brand = "Ford",
                Model = "Mustang",
                EndOfLifeMileage = 24000,
                MaintenanceInterval = 5000,
                CurrentMileage = 12000
            };

            // Act
            var result = request.ToCar();

            // Assert
            Assert.IsType<Car>(result);
        }

        [Fact]
        public void ToCar_Should_Map_UpdateCarRequest_To_Car()
        {
            // Arrange
            var request = new UpdateCarRequest
            {
                LicensePlateNumber = "123-AB-45",
                Brand = "Ford",
                Model = "Mustang",
                EndOfLifeMileage = 24000,
                MaintenanceInterval = 5000,
                CurrentMileage = 12000
            };

            // Act
            var result = request.ToCar();

            // Assert
            Assert.IsType<Car>(result);
        }

        [Fact]
        public void ToCar_Should_Set_CarId_In_UpdateCarRequest_And_Map_To_Car()
        {
            // Arrange
            var carId = Guid.NewGuid();
            var request = new UpdateCarRequest
            {
                LicensePlateNumber = "123-AB-45",
                Brand = "Ford",
                Model = "Mustang",
                EndOfLifeMileage = 24000,
                MaintenanceInterval = 5000,
                CurrentMileage = 12000
            };
            
            // Act
            var result = request.ToCar(carId);

            // Assert
            Assert.Equal(carId, request.Id);
            Assert.IsType<Car>(result);
        }
    }
}
