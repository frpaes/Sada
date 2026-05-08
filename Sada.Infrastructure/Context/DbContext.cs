using Microsoft.EntityFrameworkCore;
using Sada.Domain.Entities;
using System.Text.Json;

namespace Sada.Infrastructure.Context;

public class SadaDbContext : DbContext
{
    public SadaDbContext(
        DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Item> Itens => Set<Item>();
    public DbSet<Log> ApiLogs => Set<Log>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var logs = new List<Log>();

        var entries = ChangeTracker
            .Entries()
            .Where(x =>
                x.State == EntityState.Added ||
                x.State == EntityState.Modified ||
                x.State == EntityState.Deleted)
            .ToList();

        foreach (var entry in entries)
        {
            if (entry.Entity is Log)
                continue;

            logs.Add(new Log
            {
                Metodo = entry.Entity.GetType().Name,
                StatusCode = (int)entry.State,
                Endpoint = JsonSerializer.Serialize(entry.Entity),
                DataHora = DateTime.Now
            });
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        if (logs.Any())
        {
            ApiLogs.AddRange(logs);

            await base.SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}