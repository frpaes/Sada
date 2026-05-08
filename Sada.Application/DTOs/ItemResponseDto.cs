using Sada.Domain.Enums;

namespace Sada.Application.DTOs;

public class ItemResponseDto
{
    public Guid Id { get; set; }

    public string Titulo { get; set; }

    public string? Descricao { get; set; }

    public DateTime? DataVencimento { get; set; }

    public Status Status { get; set; }
}