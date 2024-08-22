using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using circles;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


var uriApiBase = builder.Configuration.GetValue<string>("ApiSettings:BaseUri");

if (uriApiBase is null)
     throw new InvalidOperationException("The configuration 'ApiSettings:BaseUri' value is null or empty");

builder.Services.AddTransient<CustomAuthorizationMessageHandler>();


builder.Services.AddHttpClient("AuthorizedClient",
    client => client.BaseAddress = new Uri(uriApiBase))
    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();


builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("AuthorizedClient"));


builder.Services.AddMsalAuthentication(options =>
{
     builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
     options.ProviderOptions.LoginMode = "redirect";
     options.ProviderOptions.DefaultAccessTokenScopes
         .Add("https://Circles03.onmicrosoft.com/ab4704f3-1f2a-4d04-8569-58447cb8884d/User");
});


await builder.Build().RunAsync();
