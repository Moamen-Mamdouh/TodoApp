using FluentValidation;

namespace NitajTodoApp.Application.Features.TodoItems.Commands.Create;

internal sealed class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("cannot by empty");
    }
}
