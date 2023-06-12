using FleetJourney.Application.Extensions;
using FleetJourney.Application.Services.Abstractions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FleetJourney.Application.Tests.Extensions;

public sealed class CacheServiceExtensionsTests
{
    [Fact]
    public async Task RemoveCachesAsync_ShouldRemoveCaches()
    {
        // Arrange
        var cacheServiceMock = new Mock<ICacheService<object>>();
        var cancellationToken = CancellationToken.None;
        var cacheKeys = new[] {"cacheKey1", "cacheKey2", "cacheKey3"};

        // Act
        await cacheServiceMock.Object.RemoveCachesAsync(cancellationToken, cacheKeys);

        // Assert
        foreach (string cacheKey in cacheKeys)
        {
            cacheServiceMock.Verify(x => x.RemoveAsync(cacheKey, cancellationToken), Times.Once);
        }
    }
}