using IdentityModel.Client;

namespace Area92.Authentication;

public class BearerTokenValidation
{
    public async Task AccessTokenGen()
    {
        var client = new HttpClient();
        var discoveryDocumentResponse = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
        {
            Address = discoveryDocumentResponse.TokenEndpoint,
            ClientId = "DEMON",
            ClientSecret = "DEMON",
            Scope = "API"
        });
        Console.WriteLine(tokenResponse.Json);
    }
}