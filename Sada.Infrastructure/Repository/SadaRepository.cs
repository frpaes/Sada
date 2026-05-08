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

    public async Task<Item> CreateAsync(Item entity)
    {
        await _context.Itens.AddAsync(entity);

        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<List<Item>> GetAllAsync()
    {
        return await _context.Itens.ToListAsync();
    }
      
    public async Task<Item?> GetByIdAsync(Guid id)
    {
        return await _context.Itens
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(Item entity)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Item entity)
    {
        _context.Remove(entity);

        await _context.SaveChangesAsync();
    }
}