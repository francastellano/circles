using circles.api.contracts.Weather.Queries.GetList;

var builder = WebApplication.CreateBuilder(args);


// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PolicyOne", build =>
    {
        if (builder.Environment.IsDevelopment())
            build.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        else
            build.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        // TODO: Review CORS in production
        //build.WithOrigins("http://localhost:5001").AllowAnyHeader().AllowAnyMethod();
    });
});


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use CORS
app.UseCors("PolicyOne");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();