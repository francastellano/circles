using circles.api;
using circles.application;
using circles.infrastructure;
using circles.infrastructure.Context;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

builder.Services.AddAuthorization();

builder.Services.AddFastEndpoints();

var uriAppBase = builder.Configuration.GetValue<string>("AppSettings:BaseUri");
if (string.IsNullOrEmpty(uriAppBase))
    throw new InvalidOperationException("The configuration 'AppSettings:BaseUri' value is null or empty");

builder.Services.AddCors(options =>
{
    options.AddPolicy("PolicyOne", policy =>
    {
        policy.WithOrigins(uriAppBase)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IStartupFilter, MigrationStartupFilter<CirclesDbContext>>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseCors("PolicyOne");
app.MapHealthChecks("/healthz");

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.RunAsync();