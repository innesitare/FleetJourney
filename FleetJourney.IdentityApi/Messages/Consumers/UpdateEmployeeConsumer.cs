using FleetJourney.IdentityApi.Models;
using FleetJourney.Messages.Employees;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace FleetJourney.IdentityApi.Messages.Consumers;

internal sealed class UpdateEmployeeConsumer : IConsumer<UpdateEmployee>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateEmployeeConsumer(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Consume(ConsumeContext<UpdateEmployee> consumeContext)
    {
        var user = await _userManager.FindByEmailAsync(consumeContext.Message.Employee.Email);
        if (user is not null)
        {
            await _userManager.SetEmailAsync(user, consumeContext.Message.Employee.Email);
            await _userManager.SetUserNameAsync(user, consumeContext.Message.Employee.Name);
            
            _userManager.Logger.LogInformation("{@Username} has changed credentials.", user.UserName);
        }
    }
}