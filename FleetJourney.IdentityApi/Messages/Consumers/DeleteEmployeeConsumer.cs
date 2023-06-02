using FleetJourney.IdentityApi.Models;
using FleetJourney.Messages.Employees;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace FleetJourney.IdentityApi.Messages.Consumers;

internal sealed class DeleteEmployeeConsumer : IConsumer<DeleteEmployee>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public DeleteEmployeeConsumer(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Consume(ConsumeContext<DeleteEmployee> consumeContext)
    {
        var user = await _userManager.FindByEmailAsync(consumeContext.Message.Email);
        if (user is not null)
        {
            await _userManager.DeleteAsync(user);
            _userManager.Logger.LogInformation("{@Username} has been removed.", user.UserName);
        }
    }
}