using circles.api.contracts.Circles.Queries.GetList;
using circles.application;
using circles.application.Features.Circles.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

var uriAppBase = builder.Configuration.GetValue<string>("AppSettings:BaseUri");
if (uriAppBase is null)
    throw new ArgumentNullException("The uri api base is null");

builder.Services.AddCors(options =>
{
    options.AddPolicy("PolicyOne", build =>
    {
        build.WithOrigins(uriAppBase).AllowAnyHeader().AllowAnyMethod();
     });
});

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

var app = builder.Build();

app.UseCors("PolicyOne");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/api/v1/circles", async (IMediator _mediator, [AsParameters] CircleGetListParams parameter) =>
{
    var result = await _mediator.Send(new CirclesGetListQuery(parameter));
    return result;
})
.WithName("GetCircles")
.WithOpenApi();

await app.RunAsync();