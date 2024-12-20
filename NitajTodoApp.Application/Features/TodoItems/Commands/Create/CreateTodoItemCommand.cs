using NitajTodoApp.Application.Abstractions.Messaging;

namespace NitajTodoApp.Application.Features.TodoItems.Commands.Create;

public sealed record CreateTodoItemCommand : ICommand
{
    public string Title { get; init; }
    public string? Description { get; init; }
}
