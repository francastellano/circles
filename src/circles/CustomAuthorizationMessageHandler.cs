using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace circles;

public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
{


    public CustomAuthorizationMessageHandler(IAccessTokenProvider provider,
        NavigationManager navigation,
        IConfiguration configuration)
        : base(provider, navigation)
    {

        var authUrl = configuration.GetValue<string>("ApiSettings:BaseUri");
        if (authUrl is null)
            throw new InvalidOperationException("The configuration 'ApiSettings:BaseUri' value is null or empty");

        ConfigureHandler(
            authorizedUrls: [authUrl],
            scopes: ["https://Circles03.onmicrosoft.com/ab4704f3-1f2a-4d04-8569-58447cb8884d/User"]);
    }
}