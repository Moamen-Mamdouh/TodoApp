using FluentValidation;
using NitajTodoApp.Application.Extensions.FluentValidation;
using NitajTodoApp.Domain.Entities;
using NitajTodoApp.Domain.Repositories;

namespace NitajTodoApp.Application.Features.TodoItems.Commands.MarkAsCompleted;

internal sealed class MarkTodoItemAsCompletedCommandValidator : AbstractValidator<MarkTodoItemAsCompletedCommand>
{
    public MarkTodoItemAsCompletedCommandValidator(IGenericRepository<TodoItem> todoItemRepository)
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("must by greater than 0")
            .EntityExist(todoItemRepository).WithMessage("not found!");
    }
}
