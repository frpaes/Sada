using Sada.Domain.Enums;

namespace Sada.Domain.Entities;

public class Item
{
    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public DateTime? DueDate { get; private set; }

    public Status Status { get; private set; }

    protected Item()
    {
    }

    public Item(
        string title,
        string? description,
        DateTime? dueDate,
        Status status)
    {
        Id = Guid.NewGuid();

        Update(title, description, dueDate, status);
    }

    public void Update(
        string title,
        string? description,
        DateTime? dueDate,
        Status status)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Título obrigatório.");

        Title = title;
        Description = description;
        DueDate = dueDate;
        Status = status;
    }
}