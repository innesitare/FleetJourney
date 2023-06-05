using FleetJourney.Application.Extensions;
using FleetJourney.Application.Helpers;
using FleetJourney.Application.Services.Abstractions;
using FleetJourney.Domain.EmployeeInfo;
using Mediator;

namespace FleetJourney.Application.Messages.Notifications.Employees.Handlers;

public sealed class CachingEmployeeNotificationHandler :
    INotificationHandler<CreateEmployeeMessage>,
    INotificationHandler<UpdateEmployeeMessage>,
    INotificationHandler<DeleteEmployeeMessage>
{
    private readonly ICacheService<Employee> _cacheService;

    public CachingEmployeeNotificationHandler(ICacheService<Employee> cacheService)
    {
        _cacheService = cacheService;
    }

    public async ValueTask Handle(CreateEmployeeMessage notification, CancellationToken cancellationToken)
    {
        var employee = notification.Employee;

        await _cacheService.RemoveCachesAsync(cancellationToken,
            CacheKeys.Employees.GetAll,
            CacheKeys.Employees.Get(employee.Id),
            CacheKeys.Employees.GetByEmail(employee.Email)
        );
    }

    public async ValueTask Handle(UpdateEmployeeMessage notification, CancellationToken cancellationToken)
    {
        var employee = notification.Employee;

        await _cacheService.RemoveCachesAsync(cancellationToken,
            CacheKeys.Employees.GetAll,
            CacheKeys.Employees.Get(employee.Id),
            CacheKeys.Employees.GetByEmail(employee.Email)
        );
    }

    public async ValueTask Handle(DeleteEmployeeMessage notification, CancellationToken cancellationToken)
    {
        var employeeId = notification.Id;

        await _cacheService.RemoveCachesAsync(cancellationToken,
            CacheKeys.Employees.GetAll,
            CacheKeys.Employees.Get(employeeId)
        );
    }
}