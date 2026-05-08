using Microsoft.EntityFrameworkCore;
using Sada.Domain.Entities;

namespace Sada.Infrastructure.Context;

public class SadaDbContext : DbContext
{
    public SadaDbContext(
        DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Item> Itens => Set<Item>();

}