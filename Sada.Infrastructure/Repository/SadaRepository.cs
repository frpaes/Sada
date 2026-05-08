using Microsoft.EntityFrameworkCore;
using Sada.Domain.Entities;
using Sada.Domain.Interfaces;
using Sada.Infrastructure.Context;

namespace Sada.Infrastructure.Repository;

public class SadaRepository : ISadaRepository
{
    private readonly SadaDbContext _context;

    public SadaRepository(
        SadaDbContext context)
    {
        _context = context;
    }

    public async Task<Item> CreateAsync(Item item)
    {
        await _context.Itens.AddAsync(item);

        await _context.SaveChangesAsync();

        return item;
    }

    public async Task<List<Item>> GetAllAsync()
    {
        return await _context.Itens.ToListAsync();
    }
      
    public async Task<Item?> GetByIdAsync(Guid id)
    {
        return await _context.Itens.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(Item item)
    {
        _context.Itens.Update(item);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Item item)
    {
        _context.Remove(item);

        await _context.SaveChangesAsync();
    }

    public async Task<List<Log>> GetLogs()
    {
        return await _context.ApiLogs.OrderByDescending(x => x.DataHora).ToListAsync();
    }
}