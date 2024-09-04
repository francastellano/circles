using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using circles;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.Services.AddLocalization();
//builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


// Configure supported cultures
var supportedCultures = new[] { "en", "es" };

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);
    options.SupportedCultures = supportedCultures.Select(culture => new CultureInfo(culture)).ToList();
    options.SupportedUICultures = supportedCultures.Select(culture => new CultureInfo(culture)).ToList();
});

var uriApiBase = builder.Configuration.GetValue<string>("ApiSettings:BaseUri");

if (uriApiBase is null)
    throw new InvalidOperationException("The configuration 'ApiSettings:BaseUri' value is null or empty");


builder.Services.AddHttpClient(CirclesConfiguration.DefaultHttpClient,
    client => client.BaseAddress = new Uri(uriApiBase))
    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

builder.Services.AddTransient<CustomAuthorizationMessageHandler>();

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient(CirclesConfiguration.DefaultHttpClient));

var tokenScope = builder.Configuration.GetValue<string>("TokenScope");
if (tokenScope is null)
    throw new InvalidOperationException("The configuration 'ApiSettings:tokenScope' value is null or empty");

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.LoginMode = "redirect";
    options.ProviderOptions.DefaultAccessTokenScopes
        .Add(tokenScope);
});

await builder.Build().RunAsync();
