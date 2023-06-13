# FleetJourney

Welcome to Fleet Journey! This project aims to streamline the logging of business trips taken by employees using electric cars at TheGreenMile™ company. It ensures compliance with government regulations and assists in vehicle maintenance by accurately recording trip details.

The application itself showcases the front-end and back-end combination that uses different patterns and techniques, including **Clean Architecture, CQRS, and RESTful APIs**. I emphasize the need to separate core business logic from framework and user interface concerns. This all has an impact on flexibility, testability, and loose coupling between system components.

I do welcome everybody's involvement in this project, where developers can explore and contribute to cutting-edge technologies and best practices in software development.

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

### Scrutor 

The library itself provides a substantial opportunity to register services in DI containers using assembly scanning and reflection.

### Mapping via Mapperly

.
