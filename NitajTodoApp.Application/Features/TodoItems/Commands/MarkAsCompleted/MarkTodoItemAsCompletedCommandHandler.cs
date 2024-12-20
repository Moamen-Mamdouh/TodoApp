using NitajTodoApp.Application.Abstractions.Messaging;
using NitajTodoApp.Domain.Entities;
using NitajTodoApp.Domain.Repositories;
using NitajTodoApp.Domain.Shared;

namespace NitajTodoApp.Application.Features.TodoItems.Commands.MarkAsCompleted;

internal sealed class MarkTodoItemAsCompletedCommandHandler(IGenericRepository<TodoItem> _todoItemRepository) : ICommandHandler<MarkTodoItemAsCompletedCommand>
{
    public async Task<Result> Handle(MarkTodoItemAsCompletedCommand request, CancellationToken cancellationToken)
    {
        var todoItem = await _todoItemRepository.GetByIdAsync(request.Id, cancellationToken);

        todoItem!.IsCompleted = true;

        _todoItemRepository.Update(todoItem);

        await _todoItemRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
