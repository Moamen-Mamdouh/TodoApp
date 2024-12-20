using MediatR;
using NitajTodoApp.Domain.Shared;

namespace NitajTodoApp.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
