using FleetJourney.Core.Extensions;
using FleetJourney.Core.Messages.Orchestrates;
using FleetJourney.Core.Services.Abstractions;
using FleetJourney.Core.Settings;
using FleetJourney.Core.Validation;
using FleetJourney.Infrastructure.Persistence;
using FleetJourney.Infrastructure.Persistence.Abstractions;
using FleetJourney.Infrastructure.Repositories.Abstractions;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Configuration.AddAzureKeyVault();
builder.Configuration.AddJwtBearer(builder);

builder.Services.AddControllers();
builder.Services.AddMediator();

builder.Services.AddApplicationDbContext<IApplicationDbContext, ApplicationDbContext>(
    builder.Configuration["FleetApplicationStore:ConnectionString"]!);

builder.Services.AddStackExchangeRedisCache(options => 
    options.Configuration = builder.Configuration["Redis:ConnectionString"]);

builder.Services.AddApplicationService(typeof(ICacheService<>));

builder.Services.AddApplicationService<IEmployeeService>();
builder.Services.AddApplicationService<IEmployeeRepository>();

builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<IValidationMarker>(includeInternalTypes: true);

builder.Services.AddOptions<AwsMessagingSettings>()
    .Bind(builder.Configuration.GetRequiredSection("Aws"))
    .ValidateOnStart();

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    x.AddConsumer<EmployeeOrchestrator>();

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

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSerilogRequestLogging();

app.MapControllers();
app.Run();