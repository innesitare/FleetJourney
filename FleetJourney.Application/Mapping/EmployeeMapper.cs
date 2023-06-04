using FleetJourney.Application.Contracts.Requests.Employees;
using FleetJourney.Application.Contracts.Responses.Employees;
using FleetJourney.Domain.EmployeeInfo;
using Riok.Mapperly.Abstractions;

namespace FleetJourney.Application.Mapping;

[Mapper]
public static partial class EmployeeMapper
{
    public static partial EmployeeResponse ToResponse(this Employee employee);

    public static partial Employee ToEmployee(this CreateEmployeeRequest request);
    
    public static partial Employee ToEmployee(this UpdateEmployeeRequest request);

    public static Employee ToEmployee(this UpdateEmployeeRequest request, Guid id)
    {
        request.Id = id;
        return request.ToEmployee();
    }
}