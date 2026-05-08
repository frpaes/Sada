using Sada.Domain.Entities;

namespace Sada.Domain.Interfaces;

public interface ISadaRepository
{
    Task<Item> CreateAsync(Item entity);

    Task<List<Item>> GetAllAsync();

    Task<Item?> GetByIdAsync(Guid id);

    Task UpdateAsync(Item entity);

    Task DeleteAsync(Item entity);
}