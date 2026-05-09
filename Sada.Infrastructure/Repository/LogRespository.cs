using Sada.Application.Interfaces;
using Sada.Domain.Entities;
using Sada.Infrastructure.Context;

namespace Sada.Infrastructure.Repository;

public class LogRepository : ILogService
{
    private readonly SadaDbContext _context;

    public LogRepository(SadaDbContext context)
    {
        _context = context;
    }

    public async Task SalvarAsync(Log log)
    {
        _context.Logs.Add(log);

        await _context.SaveChangesAsync();
    }
}