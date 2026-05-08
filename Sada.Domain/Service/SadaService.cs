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

    public async Task<Guid> CreateAsync(CreateItemDto itemDto)
    {
        var entity = new Item
        {
            Titulo = itemDto.Titulo,
            Descricao = itemDto.Descricao,
            DataVencimento = itemDto.DataVencimento,
            Status = itemDto.Status
        };

        await _repository.CreateAsync(entity);

        return entity.Id;
    }

    public async Task<List<ResponseItemDto>> GetAsync(Status? status, DateTime? DataVencimento)
    {
        var list = await _repository.GetAllAsync();

        if (status.HasValue)
            list = list.Where(x => x.Status == status).ToList();

        if (DataVencimento.HasValue)
            list = list.Where(x => x.DataVencimento?.Date == DataVencimento.Value.Date).ToList();

        return list.Select(x =>
            new ResponseItemDto
            {
                Id = x.Id,
                Titulo = x.Titulo,
                Descricao = x.Descricao,
                DataVencimento = x.DataVencimento,
                Status = x.Status
            }).ToList();
    }

    public async Task<ResponseItemDto> GetByIdAsync(Guid id)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item is null) throw new KeyNotFoundException("Registro não encontrado.");

        return new ResponseItemDto
        {
            Id = item.Id,
            Titulo = item.Titulo,
            Descricao = item.Descricao,
            DataVencimento = item.DataVencimento,
            Status = item.Status
        };
    }

    public async Task UpdateAsync(Guid id, UpdateItemDto dto)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item is null) throw new KeyNotFoundException("Registro não encontrado.");

        item.Titulo = dto.Titulo;
        item.Descricao = dto.Descricao;
        item.DataVencimento = dto.DataVencimento;
        item.Status = dto.Status;

        await _repository.UpdateAsync(item);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null) throw new Exception("Registro não encontrado.");

        await _repository.DeleteAsync(entity);
    }

    public async Task<List<ResponseLog>> GetLogsAsync()
    {
        var entity = await _repository.GetLogs();

        if (entity == null) throw new Exception("Logs não encontrado.");

        return entity.Select(x =>
            new ResponseLog
            {
                Metodo = x.Metodo,
                Endpoint = x.Endpoint,
                StatusCode = x.StatusCode,
                DataHora = x.DataHora
            }).ToList();
    }
}