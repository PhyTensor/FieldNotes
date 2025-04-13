using Api;
using Application.Models;
using Application.Services;

using Microsoft.AspNetCore.OpenApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
// builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);
builder.AddInfrastructure();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/notes", async (INoteService service) =>
{
    var notes = await service.GetAllAsync();
    // returns all notes projected to NoteResponse
    return Results.Ok(notes);
});

app.MapGet("/notes/{id:guid}", async (Guid id, INoteService service) =>
{
    var note = await service.GetByIdAsync(id);
    // reutns a NoteResponse
    return note is not null ? Results.Ok(note) : Results.NotFound();
});

app.MapPost("/notes", async (CreateNoteRequest request, INoteService service) =>
{
    var note = await service.CreateAsync(request);
    // returns a projection, not entity
    return Results.Created($"/notes/{note.Id}", note);
});

app.MapDelete("/notes/{id:guid}", async (Guid id, INoteService service) =>
{
    var success = await service.DeleteAsync(id);
    return success ? Results.NoContent() : Results.NotFound();
});


// Middleware for error handling
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Unhandled exception");

        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { error = "Something went wrong." });
    }
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => options
    .WithTitle("Demo API")
    .WithTheme(ScalarTheme.Saturn)
    // .WithTheme(ScalarTheme.Kepler)
    .WithDarkModeToggle(true)
    .WithClientButton(true)
    );
}

app.Run();

