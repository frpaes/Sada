using System.ComponentModel.DataAnnotations;
using Sada.Domain.Enums;

namespace Sada.Application.DTOs;

public class CreateItemDto
{
    [Required]
    public string Title { get; set; }

    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public Status Status { get; set; }
}