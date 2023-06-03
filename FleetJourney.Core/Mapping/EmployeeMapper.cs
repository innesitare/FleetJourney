using FleetJourney.Core.Contracts.Requests.Employees;
using FleetJourney.Core.Contracts.Responses.Employees;
using FleetJourney.Domain.EmployeeInfo;
using Riok.Mapperly.Abstractions;

namespace FleetJourney.Core.Mapping;

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