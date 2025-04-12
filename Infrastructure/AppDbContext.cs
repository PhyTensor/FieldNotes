using Microsoft.EntityFrameworkCore;
using Core;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Note> Notes => Set<Note>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     var stringListConverter = new ValueConverter<List<string>, string>(
    //         v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
    //         v => JsonSerializer.Deserialize<List<string>>(v) ?? new List<string>());
    //
    //     modelBuilder.Entity<Note>()
    //         .Property(n => n.Tags)
    //         .HasConversion(stringListConverter);
    // }
}
