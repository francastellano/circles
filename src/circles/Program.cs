using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using circles;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


var uriApiBase = builder.Configuration.GetValue<string>("ApiSettings:BaseUri");

if (uriApiBase is null)
    throw new InvalidOperationException("The configuration 'ApiSettings:BaseUri' value is null or empty");


builder.Services.AddHttpClient("AuthorizedClient",
    client => client.BaseAddress = new Uri(uriApiBase))
    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

builder.Services.AddTransient<CustomAuthorizationMessageHandler>();

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("AuthorizedClient"));

var tokenScope = builder.Configuration.GetValue<string>("AzureAdB2C:TokenScope");
if (tokenScope is null)
    throw new InvalidOperationException("The configuration 'ApiSettings:tokenScope' value is null or empty");

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.LoginMode = "redirect";
    options.ProviderOptions.DefaultAccessTokenScopes
        .Add(tokenScope);
});

await builder.Build().RunAsync();
