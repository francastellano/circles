using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace circles;

public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public CustomAuthorizationMessageHandler(IAccessTokenProvider provider,
        NavigationManager navigation)
        : base(provider, navigation)
    {
        ConfigureHandler(
            authorizedUrls: new[] { "http://localhost:5130", "https://circles03api.azurewebsites.net" },
            scopes: new[] { "https://Circles03.onmicrosoft.com/ab4704f3-1f2a-4d04-8569-58447cb8884d/User" });
    }
}