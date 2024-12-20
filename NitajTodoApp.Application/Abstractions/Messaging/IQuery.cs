using MediatR;
using NitajTodoApp.Domain.Shared;

namespace NitajTodoApp.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}