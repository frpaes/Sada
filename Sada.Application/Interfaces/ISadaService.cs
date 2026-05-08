using Sada.Application.DTOs;
using Sada.Domain.Enums;

namespace Sada.Application.Interfaces;

public interface ISadaService
{
    Task<Guid> CreateAsync(CreateItemDto dto);

    Task<List<ResponseItemDto>> GetAsync(Status? status, DateTime? DataVencimento);

    Task<ResponseItemDto> GetByIdAsync(Guid id);

    Task UpdateAsync(Guid id, UpdateItemDto dto);

    Task DeleteAsync(Guid id);

    Task<List<ResponseLog>> GetLogsAsync();
}