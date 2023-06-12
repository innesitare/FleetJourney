using FleetJourney.Application.Attributes;
using FleetJourney.Application.Extensions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FleetJourney.Tests.Extensions;

public sealed class ApplicationServiceExtensionsTests
{
    private interface IExampleService
    {
        void DoSomething();
    }

    private sealed class ExampleService : IExampleService
    {
        public void DoSomething()
        {
        }
    }

    [Fact]
    public void AddApplicationService_ShouldRegisterTypes_WhenInterfaceTypeProvided()
    {
        // Arrange
        var services = new ServiceCollection();
        var interfaceType = typeof(IExampleService);

        // Act
        services.AddApplicationService(interfaceType);

        // Assert
        services.Should().Contain(x => x.ServiceType == interfaceType);
        services.Should().OnlyHaveUniqueItems(x => x.ServiceType);
        services.Should().OnlyHaveUniqueItems(x => x.ImplementationType);
    }

    [Fact]
    public void AddApplicationService_ShouldNotRegisterTypesWithCachingDecoratorAttribute_WhenInterfaceTypeProvided()
    {
        // Arrange
        var services = new ServiceCollection();
        var interfaceType = typeof(IExampleService);

        // Act
        services.AddApplicationService(interfaceType);

        // Assert
        services.Where(x => x.ServiceType == interfaceType)
            .Should().NotContain(x => x.ImplementationType!.GetCustomAttributes(typeof(CachingDecorator), true).Any());
    }

    [Fact]
    public void AddApplicationService_ShouldRegisterTypes_WhenGenericTypeProvided()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationService<IExampleService>();

        // Assert
        services.Should().Contain(x => x.ServiceType == typeof(IExampleService));
        services.Should().OnlyHaveUniqueItems(x => x.ServiceType);
        services.Should().OnlyHaveUniqueItems(x => x.ImplementationType);
    }

    [Fact]
    public void AddApplicationService_ShouldNotRegisterTypesWithCachingDecoratorAttribute_WhenGenericTypeProvided()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddApplicationService<IExampleService>();

        // Assert
        services.Where(x => x.ServiceType == typeof(IExampleService))
            .Should().NotContain(x => x.ImplementationType!.GetCustomAttributes(typeof(CachingDecorator), true).Any());
    }

    [Fact]
    public void AddApplicationService_ShouldThrowArgumentNullException_WhenInterfaceTypeIsNull()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        Action act = () => services.AddApplicationService(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void AddApplicationService_ShouldThrowArgumentNullException_WhenGenericTypeIsNull()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        Action act = () => services.AddApplicationService(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }
}