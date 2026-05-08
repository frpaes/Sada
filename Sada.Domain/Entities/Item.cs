using Sada.Domain.Enums;

namespace Sada.Domain.Entities;

public class Item
{
    public Guid Id { get; set; }

    public string Titulo { get; set; }

    public string? Descricao { get; set; }

    public DateTime? DataVencimento { get; set; }

    public Status Status { get; set; }

    public Item()
    {
    }

}