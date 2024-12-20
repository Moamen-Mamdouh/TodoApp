using NitajTodoApp.Application.Features.TodoItems.Queries.Get.Dtos;

namespace NitajTodoApp.Application.Features.TodoItems.Queries.Get;

internal sealed record GetTodoItemsResponse
{
    public GetTodoItemsResponse(IReadOnlyCollection<TodoItemDto> todoItems)
    {
        TodoItems = todoItems;
    }

    public IReadOnlyCollection<TodoItemDto> TodoItems { get; init; }
}
