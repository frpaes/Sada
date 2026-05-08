using Sada.Application.DTOs;
using Sada.Application.Interfaces;
using Sada.Domain.Entities;
using Sada.Domain.Enums;
using Sada.Domain.Interfaces;

namespace Sada.Application.Service;

public class SadaService : ISadaService
{
    private readonly ISadaRepository _repository;

    public SadaService(
        ISadaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> CreateAsync(CreateItemDto dto)
    {
        var entity = new Item(
            dto.Title,
            dto.Description,
            dto.DueDate,
            dto.Status);

        await _repository.CreateAsync(entity);

        return entity.Id;
    }

    public async Task<List<ResponseItemDto>> GetAsync(Status? status, DateTime? dueDate)
    {
        var list =
            await _repository.GetAllAsync();

        if (status.HasValue)
            list = list
                .Where(x => x.Status == status)
                .ToList();

        if (dueDate.HasValue)
            list = list
                .Where(x => x.DueDate?.Date ==
                            dueDate.Value.Date)
                .ToList();

        return list.Select(x =>
            new ResponseItemDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                DueDate = x.DueDate,
                Status = x.Status
            }).ToList();
    }

    public async Task UpdateAsync(Guid id, UpdateItemDto dto)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            throw new Exception("Registro não encontrado.");

        entity.Update(
            dto.Title,
            dto.Description,
            dto.DueDate,
            dto.Status);

        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity =
            await _repository.GetByIdAsync(id);

        if (entity == null)
            throw new Exception("Registro não encontrado.");

        await _repository.DeleteAsync(entity);
    }
}