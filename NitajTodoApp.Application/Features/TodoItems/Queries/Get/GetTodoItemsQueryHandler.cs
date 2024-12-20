using MapsterMapper;
using NitajTodoApp.Application.Abstractions.Messaging;
using NitajTodoApp.Application.Features.TodoItems.Queries.Get.Dtos;
using NitajTodoApp.Application.Specifications.TodoItems;
using NitajTodoApp.Domain.Entities;
using NitajTodoApp.Domain.Repositories;
using NitajTodoApp.Domain.Shared;

namespace NitajTodoApp.Application.Features.TodoItems.Queries.Get;

internal sealed class GetTodoItemsQueryHandler(IGenericRepository<TodoItem> _todoItemRepository, IMapper _mapper) : IQueryHandler<GetTodoItemsQuery, GetTodoItemsResponse>
{
    public Task<Result<GetTodoItemsResponse>> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
    {
        var items = _todoItemRepository.GetWithSpec(new TodoItemsByIsCompletedSpecification(isCompleted: request.GetPendingOnly ? false : null));

        var itemsDtos = _mapper.Map<List<TodoItemDto>>(items.data);

        return Task.FromResult(Result.Success(new GetTodoItemsResponse(itemsDtos), items.count));
    }
}
