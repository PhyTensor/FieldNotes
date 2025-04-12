using Api;
using Application.Models;
using Application.Services;

var builder = WebApplication.CreateBuilder(args);
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

app.Run();

