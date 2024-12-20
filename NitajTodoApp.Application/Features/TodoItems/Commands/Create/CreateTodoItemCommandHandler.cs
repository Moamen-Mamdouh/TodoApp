using MapsterMapper;
using NitajTodoApp.Application.Abstractions.Messaging;
using NitajTodoApp.Domain.Entities;
using NitajTodoApp.Domain.Repositories;
using NitajTodoApp.Domain.Shared;

namespace NitajTodoApp.Application.Features.TodoItems.Commands.Create;

public sealed class CreateTodoItemCommandHandler(IMapper _mapper, IGenericRepository<TodoItem> _todoItemRepository) : ICommandHandler<CreateTodoItemCommand>
{
    public async Task<Result> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todoItem = _mapper.Map<TodoItem>(request);

        await _todoItemRepository.AddAsync(todoItem);

        await _todoItemRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
