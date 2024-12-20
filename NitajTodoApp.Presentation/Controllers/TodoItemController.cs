using MediatR;
using Microsoft.AspNetCore.Mvc;
using NitajTodoApp.Application.Features.TodoItems.Commands.Create;
using NitajTodoApp.Application.Features.TodoItems.Commands.MarkAsCompleted;
using NitajTodoApp.Application.Features.TodoItems.Queries.Get;
using NitajTodoApp.Presentation.Abstractions;

namespace NitajTodoApp.Presentation.Controllers;


[Route("api/todo")]
public sealed class TodoItemController : ApiController
{
    public TodoItemController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoItemCommand command, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(command, cancellationToken);

        return HandleResult(response);
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new GetTodoItemsQuery();

        var response = await Sender.Send(query, cancellationToken);

        return HandleResult(response);
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPending(CancellationToken cancellationToken)
    {
        var query = new GetTodoItemsQuery(getPendingOnly: true);

        var response = await Sender.Send(query, cancellationToken);

        return HandleResult(response);
    }

    [HttpPut("{id:int}/complete")]
    public async Task<IActionResult> MarkAsCompleted(int id, CancellationToken cancellationToken)
    {
        var command = new MarkTodoItemAsCompletedCommand(id);

        var response = await Sender.Send(command, cancellationToken);

        return HandleResult(response);
    }
}
