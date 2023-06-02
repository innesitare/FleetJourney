using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace FleetJourney.Core.Services;

internal sealed class PrefixKeyVaultSecretManager : KeyVaultSecretManager
{
    private readonly string _prefix;

    public PrefixKeyVaultSecretManager(string prefix)
    {
        _prefix = $"{prefix}-";
    }

    override public bool Load(SecretProperties secret)
    {
        return secret.Name.StartsWith(_prefix);
    }

    override public string GetKey(KeyVaultSecret secret)
    {
        return secret.Name[_prefix.Length..]
            .Replace("--", ConfigurationPath.KeyDelimiter);
    }
}