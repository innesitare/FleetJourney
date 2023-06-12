using Azure.Security.KeyVault.Secrets;
using FleetJourney.Application.Helpers;
using FluentAssertions;
using Xunit;

namespace FleetJourney.Application.Tests.Helpers;

public sealed class PrefixKeyVaultSecretManagerTests
{
    [Theory]
    [InlineData("prefix")]
    public void Load_Should_ReturnTrueForSecret_WithCorrectPrefix(string prefix)
    {
        // Arrange
        var secret = new SecretProperties("prefix-secret-name");

        // Act
        var manager = new PrefixKeyVaultSecretManager(prefix);
        bool result = manager.Load(secret);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("prefix")]
    public void Load_Should_ReturnFalseForSecret_WithIncorrectPrefix(string prefix)
    {
        // Arrange
        var secret = new SecretProperties("other-prefix-secret-name");

        // Act
        var manager = new PrefixKeyVaultSecretManager(prefix);
        bool result = manager.Load(secret);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("prefix", "prefix-secret-name", "secret-name")]
    [InlineData("prefix", "prefix-secret--name", "secret:name")]
    public void GetKey_Should_ReturnCorrectKey(string prefix, string secretName, string expectedKey)
    {
        // Arrange
        var secret = new KeyVaultSecret(secretName, string.Empty);

        // Act
        var manager = new PrefixKeyVaultSecretManager(prefix);
        string key = manager.GetKey(secret);

        // Assert
        key.Should().Be(expectedKey);
    }
}