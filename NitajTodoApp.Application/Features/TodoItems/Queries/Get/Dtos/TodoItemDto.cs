namespace NitajTodoApp.Application.Features.TodoItems.Queries.Get.Dtos;

internal sealed record TodoItemDto
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public bool IsCompleted { get; init; }
    public DateTime CreatedOnUtc { get; init; }
    public DateTime? ModifiedOnUtc { get; init; }
}
