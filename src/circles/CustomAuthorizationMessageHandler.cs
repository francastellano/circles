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

        var authScope = configuration.GetValue<string>("TokenScope");
        if (authScope is null)
            throw new InvalidOperationException("The configuration 'AzureAdB2C:TokenScope' value is null or empty");

        ConfigureHandler(
            authorizedUrls: [authUrl],
            scopes: [authScope]);
    }
}