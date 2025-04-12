using Api;

var builder = WebApplication.CreateBuilder(args);

builder.AddInfrastructure();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
