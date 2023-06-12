using FleetJourney.Application.Contracts.Requests.Employees;
using FleetJourney.Application.Contracts.Responses.Employees;
using FleetJourney.Application.Mapping;
using FleetJourney.Domain.EmployeeInfo;
using Xunit;

namespace FleetJourney.Application.Tests.Mapping;

public sealed class EmployeeMapperTests
{
    [Fact]
    public void ToResponse_Should_Map_Employee_To_EmployeeResponse()
    {
        // Arrange
        var employee = new Employee
        {
            Name = "John",
            LastName = "Doe",
            Email = "johndoe@example.com",
            Birthdate = new DateOnly(1990, 1, 1)
        };

        // Act
        var result = employee.ToResponse();

        // Assert
        Assert.IsType<EmployeeResponse>(result);
    }

    [Fact]
    public void ToEmployee_Should_Map_CreateEmployeeRequest_To_Employee()
    {
        // Arrange
        var request = new CreateEmployeeRequest
        {
            Name = "John",
            LastName = "Doe",
            Email = "johndoe@example.com",
            Birthdate = new DateOnly(1990, 1, 1)
        };

        // Act
        var result = request.ToEmployee();

        // Assert
        Assert.IsType<Employee>(result);
    }

    [Fact]
    public void ToEmployee_Should_Map_UpdateEmployeeRequest_To_Employee()
    {
        // Arrange
        var request = new UpdateEmployeeRequest
        {
            Name = "John",
            LastName = "Doe",
            Email = "johndoe@example.com",
            Birthdate = new DateOnly(1990, 1, 1)
        };

        // Act
        var result = request.ToEmployee();

        // Assert
        Assert.IsType<Employee>(result);
    }

    [Fact]
    public void ToEmployee_Should_Set_Id_In_UpdateEmployeeRequest_And_Map_To_Employee()
    {
        // Arrange
        var request = new UpdateEmployeeRequest
        {
            Name = "John",
            LastName = "Doe",
            Email = "johndoe@example.com",
            Birthdate = new DateOnly(1990, 1, 1)
        };
        var id = Guid.NewGuid();

        // Act
        var result = request.ToEmployee(id);

        // Assert
        Assert.Equal(id, request.Id);
        Assert.IsType<Employee>(result);
    }
}