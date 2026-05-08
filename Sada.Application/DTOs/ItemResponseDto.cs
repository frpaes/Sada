using Sada.Domain.Enums;

namespace Sada.Application.DTOs;

public class ItemResponseDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public Status Status { get; set; }
}