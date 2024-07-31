using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using circles;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var uriApiBase = builder.Configuration.GetValue<string>("ApiSettings:BaseUri");
if (uriApiBase is null)
     throw new ArgumentNullException(nameof(uriApiBase), "The URI API base is null");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(uriApiBase) });

await builder.Build().RunAsync();
