using System.ComponentModel.DataAnnotations;
using Sada.Domain.Enums;

namespace Sada.Application.DTOs;

public class CreateItemDto
{
    [Required(ErrorMessage = "Título é obrigatório.")]
    [StringLength(60, MinimumLength = 3, ErrorMessage = "Título deve ter entre 3 e 60 caracteres.")]
    public string Titulo { get; set; }

    [StringLength(100, ErrorMessage = "Descrição deve ter no máximo 100 caracteres.")]
    public string? Descricao { get; set; }

    [DataType(DataType.Date, ErrorMessage = "Data inválida.")]
    public DateTime? DataVencimento { get; set; }

    [Required(ErrorMessage = "Status é obrigatório.")]
    [EnumDataType(typeof(Status), ErrorMessage = "Status inválido.")]
    public Status Status { get; set; }
}