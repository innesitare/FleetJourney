import { InteractiveBrowserCredential } from "@azure/identity";
import { SecretClient } from "@azure/keyvault-secrets";

export async function getKeyVaultSecrets(): Promise<{ [key: string]: string }> {
    const vaultName = "FleetJourney";
    const credential = new InteractiveBrowserCredential();
    const client = new SecretClient(`https://${vaultName}.vault.azure.net`, credential);

    const secrets = client.listPropertiesOfSecrets();

    const secretValues: { [key: string]: string } = {};
    for await (const secret of secrets) {
        const secretValue = await client.getSecret(secret.name);
        secretValues[secret.name] = secretValue.value!;
    }

    return secretValues;
}
