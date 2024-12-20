namespace NitajTodoApp.Domain.Shared;

public class Result
{

    /// <summary>
    /// Indicates whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }
    /// <summary>
    /// Gets a message describing the result of the operation.
    /// </summary>
    public string Message { get; }
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class for a successful operation.
    /// </summary>
    protected internal Result(bool isSuccess, string? message = null)
    {
        IsSuccess = isSuccess;
        Message = message ?? (isSuccess ? "Success" : "Failure");
    }

    /// <summary>
    /// Creates a successful result without any additional data.
    /// </summary>
    public static Result Success()
        => new(true);

    /// <summary>
    /// Creates a successful result with the specified value.
    /// </summary>
    public static Result<TValue> Success<TValue>(TValue value)
        => new(value);

    /// <summary>
    /// Creates a successful result with the specified value and total count.
    /// </summary>
    public static Result<TValue> Success<TValue>(TValue value, int totalCount)
        => new(value, totalCount);

    /// <summary>
    /// Creates a failure result with the specified message.
    /// </summary>
    public static Result Failure(string message)
        => new(false, message);

    /// <summary>
    /// Creates a failure result with the specified message.
    /// </summary>
    public static Result<TValue> Failure<TValue>(string message) =>
       new(default, message);

    /// <summary>
    /// Creates a result based on the specified value.
    /// If the value is not null, creates a successful result; otherwise, creates a failure result.
    /// </summary>
    public static Result<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>("null value");
}
