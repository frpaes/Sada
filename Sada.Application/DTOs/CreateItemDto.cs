using System.ComponentModel.DataAnnotations;
using Sada.Domain.Enums;

namespace Sada.Application.DTOs;

public class CreateItemDto
{
    [Required]
    public string Titulo { get; set; }

    public string? Descricao { get; set; }

    public DateTime? DataVencimento { get; set; }

    public Status Status { get; set; }
}