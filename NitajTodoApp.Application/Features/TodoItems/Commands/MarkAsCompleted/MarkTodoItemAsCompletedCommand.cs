using NitajTodoApp.Application.Abstractions.Messaging;

namespace NitajTodoApp.Application.Features.TodoItems.Commands.MarkAsCompleted;

public sealed record MarkTodoItemAsCompletedCommand : ICommand
{
    public MarkTodoItemAsCompletedCommand(int id)
    {
        Id = id;
    }

    public int Id { get; init; }
}
