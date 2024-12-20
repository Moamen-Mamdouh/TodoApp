using NitajTodoApp.Domain.Primitives;

namespace NitajTodoApp.Domain.Entities;

public sealed class TodoItem : Entity, IAuditableEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
