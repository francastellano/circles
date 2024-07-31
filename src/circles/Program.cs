using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using circles;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


var uriApiBase = builder.Configuration.GetValue<string>("ApiSettings:BaseUri");

if (uriApiBase is null)
     throw new InvalidOperationException("The configuration 'ApiSettings:BaseUri' value is null or empty");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(uriApiBase) });

await builder.Build().RunAsync();
