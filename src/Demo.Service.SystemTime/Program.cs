using Demo.Service.SystemTime;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Get the simulated system time
app.MapGet("/SystemTime", () =>
{
    if (!SimulatedSystemTime.DateTimeUtc.HasValue)
    {
        return Results.BadRequest("No simulated system time has been set");
    }
    return Results.Ok(SimulatedSystemTime.DateTimeUtc.Value.ToString("o"));
});

// Set the simulated system time
app.MapPost("/SystemTime", (DateTime systemTime) =>
{
    if (systemTime.Kind != DateTimeKind.Utc)
    {
        return Results.BadRequest("The DateTimeKind must be UTC eg (2020-01-01T09:30:10Z)");
    }
    
    SimulatedSystemTime.DateTimeUtc = systemTime;
    return Results.Ok(SimulatedSystemTime.DateTimeUtc.Value);
});

// Clear the simulated system time
app.MapDelete("/SystemTime", () =>
{
    SimulatedSystemTime.DateTimeUtc = null;
    return Results.Ok();
});

app.Run();