using FluentValidation;
using MediatR;
using NitajTodoApp.Domain.Shared;

namespace NitajTodoApp.Application.Behaviors;

public sealed class ValidationPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var validationTasks = _validators
            .Select(validator => validator.ValidateAsync(request, cancellationToken))
            .ToList();

        await Task.WhenAll(validationTasks);

        var errors = validationTasks
            .SelectMany(validationTask => validationTask.Result.Errors)
            .Where(validationFailure => validationFailure != null)
            .Select(failure
                    => $"{failure.PropertyName} {failure.ErrorMessage}")
            .Distinct()
            .ToArray();

        if (errors.Any())
            return CreateValidationResult<TResponse>(errors);

        return await next();
    }

    private static TResult CreateValidationResult<TResult>(string[] errorMessages)
        where TResult : Result
    {
        if (typeof(TResult).IsAssignableFrom(typeof(Result)))
            return (ValidationResult.WithErrors(errorMessages) as TResult)!;

        object validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult<TResult>.WithErrors))!
            .Invoke(null, [null, errorMessages])!;

        return (TResult)validationResult;
    }
}