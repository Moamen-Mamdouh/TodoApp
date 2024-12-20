using NitajTodoApp.Application.Abstractions.Messaging;

namespace NitajTodoApp.Application.Features.TodoItems.Queries.Get;

public sealed record GetTodoItemsQuery : IQuery<GetTodoItemsResponse>
{
    public GetTodoItemsQuery(bool getPendingOnly = false)
    {
        GetPendingOnly = getPendingOnly;
    }

    public bool GetPendingOnly { get; init; }
}
