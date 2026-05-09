using Microsoft.EntityFrameworkCore;
using Sada.Domain.Entities;
using System.Text.Json;

namespace Sada.Infrastructure.Context;

public class SadaDbContext : DbContext
{
    public SadaDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Item> Itens => Set<Item>();
    public DbSet<Log> Logs => Set<Log>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var logs = ChangeTracker
            .Entries<Item>()
            .Where(e =>
                e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.State == EntityState.Deleted)
            .Select(e => new Log
            {
                Metodo = e.State.ToString(),
                StatusCode = (int)e.State,
                Json = JsonSerializer.Serialize(e.Entity),
                DataHora = DateTime.Now
            })
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        if (logs.Count > 0)
        {
            await Logs.AddRangeAsync(logs, cancellationToken);
            await base.SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}