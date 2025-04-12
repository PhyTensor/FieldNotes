using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api;

public static class DependncyInjection
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        // Trying to get the connection string from the environment variable
        var envConnString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

        var fallback = builder.Configuration.GetConnectionString("DefaultConnection");

        var connectionString = string.IsNullOrWhiteSpace(envConnString) ? fallback : envConnString;


        // var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Host=localhost;Database=fieldnotes;Username=postgres;Password=postgres";

        builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));


        return builder;
    }
}
