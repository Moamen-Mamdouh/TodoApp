using NitajTodoApp.Domain.Entities;
using NitajTodoApp.Domain.Specifications;

namespace NitajTodoApp.Application.Specifications.TodoItems;

internal sealed class TodoItemsByIsCompletedSpecification : Specification<TodoItem>
{
    public TodoItemsByIsCompletedSpecification(bool? isCompleted = null)
    {
        if (isCompleted != null)
            AddCriteria(item => item.IsCompleted == isCompleted);

        EnableTotalCount();
    }
}
