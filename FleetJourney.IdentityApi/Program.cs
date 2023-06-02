using FleetJourney.Core.Extensions;
using FleetJourney.Core.Settings;
using FleetJourney.IdentityApi.Extensions;
using FleetJourney.IdentityApi.Messages.Consumers;
using FleetJourney.IdentityApi.Models;
using FleetJourney.IdentityApi.Persistence;
using FleetJourney.IdentityApi.Persistence.Abstractions;
using FleetJourney.IdentityApi.Services.Abstractions;
using FleetJourney.IdentityApi.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Configuration.AddAzureKeyVault();

builder.Services.AddControllers();
builder.Services.AddMediator();

builder.Services.AddApplicationDbContext<IIdentityDbContext, IdentityDbContext>(builder.Configuration["FleetIdentityDb:ConnectionString"]!);

builder.Services.AddApplicationService<IAuthService<ApplicationUser>>();
builder.Services.AddApplicationService<ITokenWriter<ApplicationUser>>();

builder.Services.AddOptions<JwtSettings>()
    .Bind(builder.Configuration.GetRequiredSection("Jwt"))
    .ValidateOnStart();

builder.Services.AddOptions<AwsMessagingSettings>()
    .Bind(builder.Configuration.GetRequiredSection("AWS"))
    .ValidateOnStart();

builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<IValidationMarker>();

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    x.AddConsumer<DeleteEmployeeConsumer>();
    x.AddConsumer<UpdateEmployeeConsumer>();

    x.UsingAmazonSqs((context, config) =>
    {
        var awsSettings = context.GetRequiredService<IOptions<AwsMessagingSettings>>().Value;
        config.Host(awsSettings.Region, hostConfig =>
        {
            hostConfig.AccessKey(awsSettings.AccessKey);
            hostConfig.SecretKey(awsSettings.SecretKey);
        });

        config.ConfigureEndpoints(context);

        config.UseNewtonsoftJsonSerializer();
        config.UseNewtonsoftJsonDeserializer();
    });
});

builder.Services.AddIdentityConfiguration();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.MapControllers();
app.Run();