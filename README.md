# FleetJourney

Welcome to Fleet Journey! This project aims to streamline the logging of business trips taken by employees using electric cars at TheGreenMile™ company. It ensures compliance with government regulations and assists in vehicle maintenance by accurately recording trip details.

The application itself showcases the front-end and back-end combination that uses different patterns and techniques, including **Clean Architecture, CQRS, and RESTful APIs**. I emphasize the need to separate core business logic from framework and user interface concerns. This all has an impact on flexibility, testability, and loose coupling between system components.

I do welcome everybody's involvement in this project, where developers can explore and contribute to cutting-edge technologies and best practices in software development.

# Frontend implementation

As for the frontend part, the simple dashboard was implemented using React.ts. Logic is being separated and needed data is being fetched using services. Requests are basically being sent to API backend gateway, and then the responses are being deserialized and used to fill out the React pages.

Vite.js is used as a build tool that aims to improve the developer experience for development with the local machine, and for the build of optimized assets for production.

### Environment varibles

`REACT_APP_AUTH0_DOMAIN`: The domain where your Auth0 account is hosted.\
`REACT_APP_AUTH0_CALLBACK_URL`: The callback URL that Auth0 redirects to after the user performs any of authentication-related actions.\
`REACT_APP_AUTH0_CLIENT_ID`: The client ID of your Auth0 application.\
`REACT_APP_AUTH0_AUDIENCE`: The audience value for your Auth0 application.\
`REACT_APP_API_SERVER_URL`: The representation of the URL of application's server-side API.\

Make sure to provide the necessary configuration values either through the .env file or by setting the corresponding environment variables.

### Getting started

To run the frontend of FleetJourney, please follow these step-by-step instructions:

1. Open a terminal or command prompt.

2. Navigate to the `FleetJourney.Web` directory in the project using the `cd` command in project root folder:
```
cd FleetJourney.Web
```

3. Install the required packages by running the following command:
```
npm install
```

This command will fetch all the necessary packages and dependencies specified in the `package.json` file.

4. (Optional) If you want to update the installed libraries to their latest versions, you can run:
```
npm update
```

This command will update the libraries if there are newer versions available.

5. To run the frontend, execute the following script:
```
npm run build
```

This command will build the frontend assets and start the frontend server.

6. If you have made any changes to ports or related path data, make sure to update the `vite.config.js` file accordingly. This file contains the configuration for the frontend, including the localhost port on which it will run.

> Note: By default, the frontend runs on `localhost:4040`.


# Backend implementation

## Clean Architecture principles 

In this project, I have implemented its principles to ensure an organized codebase. It enhances maintainability, and extensibility of the application over time. Moreover, it provides flexibility and facilitates the adoption of new technologies without disrupting the core business logic.

Here are the key aspects of our implementation:

- Decoupling Core Business Logic: separation ensures that changes and updates can be made to specific layers without impacting the entire system. 

- Dependency Inversion Principle: I adhere to the Dependency Inversion Principle, which aims to minimize coupling between components. Following this principle, high-level modules do not depend on low-level modules directly. 

- Distinct Layers: Our implementation organizes the codebase into separate layers, each with a well-defined responsibility:
    - Domain Layer: A core of the application, housing the entities. It remains independent of other layers, with no knowledge of infrastructure or frameworks.
    - Application Layer: Represents a mediator, facilitating communication between the user interface and the domain layer.
    - Infrastructure Layer: Handles external dependencies such as databases, APIs, and frameworks.
    - User Interface Layer: Represents the user-facing part of the application, encompassing web interfaces, APIs, or command-line interfaces.

- Code Organization: Implementation follows a project or folder-based organization for each layer. It enhances code readability and collaboration by making it easier to locate and modify specific components.

## Patterns and decent techniques

### CQRS & Mediator 

Command and Query Responsibility Segregation pattern provides a separation between read and write operations, improves performance and scalability by optimizing each operation independently, wheres a Mediator implementation by @martinothamar is used here for in-process messaging.

`CreateEmployeeCommand` represents a command that is responsible for creating a new employee in the system. It is handled by the `CreateEmployeeCommandHandler`, which takes care of persisting the employee data using the `IEmployeeRepository`. After the employee is successfully created, a `CreateEmployeeMessage` is published using the `IPublisher`.

```csharp
public sealed class CreateEmployeeCommand : ICommand<bool>
{
    public Employee Employee { get; init; }
}
```
```csharp
internal sealed class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPublisher _publisher;

    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IPublisher publisher)
    {
        _employeeRepository = employeeRepository;
        _publisher = publisher;
    }

    public async ValueTask<bool> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        bool created = await _employeeRepository.CreateAsync(command.Employee, cancellationToken);
        if (created)
        {
            await _publisher.Publish(new CreateEmployeeMessage
            {
                Employee = command.Employee
            }, cancellationToken);
        }

        return created;
    }
}
```

Likewise with queries. Each of them demonstrates a model that obtains an entity using a key.

`GetEmployeeByEmailQuery` represents a query that retrieves an employee based on their email address. The `GetEmployeeByEmailQueryHandler` handles this query by fetching the employee data from the `IEmployeeRepository`.

```csharp
public sealed class GetEmployeeByEmailQuery : IQuery<Employee?>
{
    public string Email { get; init; }
}
```
```csharp
internal sealed class GetEmployeeByEmailQueryHandler : IQueryHandler<GetEmployeeByEmailQuery, Employee?>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeByEmailQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async ValueTask<Employee?> Handle(GetEmployeeByEmailQuery query, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByEmailAsync(query.Email, cancellationToken);

        return employee;
    }
}
```
---

### MassTransit message library using **Amazon SQS** for message queuing service

I have implemented orchestrators that consumes different MassTransit messages related to operations. Let's take a closer look at the code:

As for an example, there's a `CreateEmployee` message, that contains necessary information to create a new employee. There are also implemented other message-classes for all the CRUD operations.

```csharp
public sealed class CreateEmployee
{
    public required string Email { get; init; }

    public required string Name { get; init; }

    public required string LastName { get; init; }

    public required DateOnly Birthdate { get; init; }
}
```

Next, there's an `EmployeeOrchestrator` class that implements the `IConsumer` interface for three different message types: `CreateEmployee`, `UpdateEmployee`, and `DeleteEmployee`. This allows the orchestrator to handle messages of these types.

```csharp
internal sealed class EmployeeOrchestrator :
    IConsumer<CreateEmployee>,
    IConsumer<UpdateEmployee>,
    IConsumer<DeleteEmployee>
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeeOrchestrator> _logger;

    public EmployeeOrchestrator(IEmployeeService employeeService, ILogger<EmployeeOrchestrator> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CreateEmployee> context)
    {
        var message = context.Message;
        _logger.LogInformation("Creating employee with email: {Email}", message.Email);

        var employee = message.ToEmployee();
        await _employeeService.CreateAsync(employee, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<UpdateEmployee> context)
    {
        var message = context.Message;
        _logger.LogInformation("Updating employee with email: {Email}", message.Employee.Email);

        await _employeeService.UpdateAsync(message.Employee, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DeleteEmployee> context)
    {
        var message = context.Message;
        _logger.LogInformation("Deleting employee with id: {Id}", message.Id);

        await _employeeService.DeleteAsync(message.Id, context.CancellationToken);
    }
}
```

Since the business logic doesn't require any state tracking, it makes no sense to implement Saga and State machines. That's why orchestration was used here in order to handle consumers. This combination enables efficient messaging within project APIs.

---

### Repository design pattern 

This helps in abstracting the data access layer from the rest of the application. It provides a consistent interface for interacting with data, enabling easier database operations.

In the provided code snippet, there is a definition of the `IRepository<TEntity, TKey>` interface, which serves as the main template for repository APIs.

```csharp
public interface IRepository<TEntity, in TKey>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<TEntity?> GetAsync(TKey key, CancellationToken cancellationToken);

    Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken);

    Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(TKey key, CancellationToken cancellationToken);
}
```

By implementing the `IRepository<TEntity, TKey>` interface, you can create concrete repository classes that provide the actual implementation for interacting with a specific data storage mechanism, such as a database

---

### Service methods & caching

All the APIs have implemented services that work with repositories. Additionally, they are all managing caching via `ICacheService` in FleetJourney.Application.

As an example, there's one of many service methods that directly fetches all the Employees via `IEmployeeRepository`. Caching itself is implemented via Mediator Notifications handling.

```csharp
public Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken)
{
    return _cacheService.GetAllOrCreateAsync(CacheKeys.Employees.GetAll, async () =>
    {
        var employees = await _sender.Send(new GetAllEmployeesQuery(), cancellationToken);

        return employees;
    }, cancellationToken);
}
```

---

### Scrutor 

The library itself provides a substantial opportunity to register services in DI containers using assembly scanning and reflection.

In our case, I have implemented a service registration via creating an extension method that wraps up the Scrutor methods.

```csharp
public static IServiceCollection AddApplicationService<TInterface>(
    this IServiceCollection services,
    ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
{
    services.Scan(scan => scan
        .FromAssemblyOf<TInterface>()
        .AddClasses(classes =>
        {
            classes.AssignableTo<TInterface>()
                .WithoutAttribute<CachingDecorator>();
        })
        .AsImplementedInterfaces()
        .WithLifetime(serviceLifetime));

    return services;
}
```

Afterwards, whenever an interface is parsed to the extension method as a parameter, Scrutor scans all classes from its assembly and implements each of the derived classes as an implementation.

```csharp
builder.Services.AddApplicationService<IEmployeeRepository>();
```

---

### Mapping via Mapperly

Firstly, all the contract mapping was implemented by myself without using any of external libraries. As for now, I have decided to move to using Mapperly. 

This is a a source generator for generating object mappings. It creates the mapping code at **build time**, so there is minimal overhead at runtime.

```csharp
[Mapper]
public static partial class EmployeeMapper
{
    public static partial EmployeeResponse ToResponse(this Employee employee);

    ...
}
```

By leveraging Mapperly, we do benefit from automatic code generation for mapping operations, reducing the manual effort required to write and maintain mapping code.

## Getting started

### Environment variables:

You must set up the following in your secrets in order to run this project:

`AZURE_CLIENT_ID`: The client ID of your Azure application.\
`AZURE_CLIENT_SECRET`: The client secret of your Azure application.\
`AZURE_TENANT_ID`: The ID of your Azure tenant.\
`AZURE_VAULT_NAME`: The name of your Azure Key Vault.

For authentication and accessing the Azure Key Vault in the project, these environment variables are required. Make sure to configure your secrets or environment such that the values of these variables are appropriate.

### To run the application in Docker using K8s pods, follow these steps:

Use the following link to access the Docker images: https://hub.docker.com/u/plaam.

Two versions of tags are available: 1.X.X and 2.X.X. Tags starting with 1.X.X are built for AMD64 architecture, whereas 2.X.X are used for ARM64.

### Configuration settings:

`AWS`: Login Credentials\
`Redis`: Connection String\
`Auth0:` Settings\
`JWT`: Settings

### To deploy and delete the pods to Kubernetes, run the following command in the root folder:

1. Clone the repository from GitHub by executing the following command in your terminal:
```
git clone https://github.com/plaam/FleetJourney.git
```

2. Set up the required environment variables by creating a `.env` file for front-end and/or utilizing the Kubernetes secrets mechanism for back-end. 

> Note: Needed variables for configuring the project can be found in the "Environment Variables" section of the documentation.

3. Open a command prompt or terminal and navigate to the project root directory.

4. Start the deployment of the microservices to Kubernetes by running the following command:
```
kubectl apply -R -f ./FleetJourney.Deploy
```

> Note: Please ensure that you have Kubernetes installed and configured properly on your local machine before running this command.

5. If you want to delete all pods, you can execute the following command in your terminal:
```
kubectl delete -R -f ./FleetJourney.Deploy
```

This command will remove all deployed microservices and associated pods from your Kubernetes cluster.

**Additionally, don't forget that all requests should be made to the API Gateway, which will eventually redirect them to the appropriate controller**.

## Postman Configuration

You will find the Postman collections file in specific directory `FleetJourney.PostmanConfiguration`. These files contain three collections of API endpoints and associated requests that can be imported into Postman for testing and interacting with FleetJourney's APIs.

To import the Postman collection:

1. Launch Postman.
2. Click on the "Import" button located in the top-left corner.
3. Select the option to "Import File" and choose the "FleetJourney.PostmanConfiguration.postman_collection.json" specific file from the directory.
4. Postman will import the selected collection, and you will be able to see the available requests and their associated details.


After importing the collection, you can explore the endpoints, customize the request parameters, and execute the requests against FleetJourney's APIs directly from Postman. This enables you to easily test and interact with the functionality provided by the project.

## Contributing

Please be sure to adhere to the project's code style and current patterns. Create an issue and seek for assistance if you're unsure about something.

Additionally, I advise creating a pull request to discuss your implemented changes and adjustments so I can ensure that your work is in keeping with the objectives and course of the project.
