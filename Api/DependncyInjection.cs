using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api;

public static class DependncyInjection
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Host=localhost;Database=fieldnotes;Username=postgres;Password=postgres";

        builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));


        return builder;
    }
}
