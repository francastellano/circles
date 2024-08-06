using circles.api;
using circles.api.contracts.Circles.Commands.Add;
using circles.api.contracts.Circles.Queries.GetList;
using circles.application;
using circles.application.Features.Circles.Commands.Add;
using circles.application.Features.Circles.GetList;
using circles.infrastructure;
using circles.infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

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

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IStartupFilter, MigrationStartupFilter<CirclesDbContext>>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseCors("PolicyOne");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();


app.MapGet("/api/v1/circles", async (IMediator _mediator, [AsParameters] CircleGetListParams parameter) =>
{
    var result = await _mediator.Send(new CirclesGetListQuery(parameter));
    return result;
})
.WithName("GetCircles")
.WithOpenApi();


app.MapPost("/api/v1/circles", async (IMediator _mediator, [FromBody] CircleAddParams parameter) =>
{
    await _mediator.Send(new CirclesAddCommand(parameter));
    return Results.Created();
})
.WithName("AddCircle")
.WithOpenApi();

await app.RunAsync();