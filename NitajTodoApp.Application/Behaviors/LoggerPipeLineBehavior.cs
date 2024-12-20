using MediatR;
using Microsoft.Extensions.Logging;
using NitajTodoApp.Domain.Shared;
using System.Diagnostics;

namespace NitajTodoApp.Application.Behaviors;

public sealed class LoggerPipeLineBehavior<TRequest, TResponse>(ILogger<TRequest> _logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = string.Concat(
            typeof(TRequest).Name
            .Select((character, index) => index > 0 && char.IsUpper(character) ? (" " + character) : character.ToString()));

        _logger.LogInformation($"{request.ToString()} | Begin To Execute {requestName}");

        Stopwatch stopwatch = Stopwatch.StartNew();
        TResponse? response;
        try
        {
            response = await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{request.ToString()} | Error In {requestName} | {ex.Message}");
            response = (TResponse?)Result.Failure($"{ex.Message} | {requestName}");
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogDebug($"{request.ToString()} | {requestName} Cost Time : {stopwatch.Elapsed.TotalMilliseconds} ms");
            _logger.LogInformation($"{request.ToString()} | {requestName} has been finished successfully");
        }
        return response;
    }
}
