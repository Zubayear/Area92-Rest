// using IdentityModel.Client;
//
// namespace Client
// {
//     public class Program
//     {
//         private static async Task Main()
//         {
//             // discover endpoints from metadata
//             var client = new HttpClient();
//
//             var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
//             if (disco.IsError)
//             {
//                 Console.WriteLine(disco.Error);
//                 return;
//             }
//
//             // request token
//             var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
//             {
//                 Address = disco.TokenEndpoint,
//                 ClientId = "DEMON",
//                 ClientSecret = "DEMON",
//                 Scope = "API"
//             });
//             
//             if (tokenResponse.IsError)
//             {
//                 Console.WriteLine(tokenResponse.Error);
//                 return;
//             }
//
//             Console.WriteLine(tokenResponse.Json);
//             Console.WriteLine("\n\n");
//         }
//     }
// }