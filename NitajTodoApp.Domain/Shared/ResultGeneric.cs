namespace NitajTodoApp.Domain.Shared;

public class Result<TData> : Result
{
    /// <summary>
    /// Gets the total count associated with the result.
    /// </summary>
    public int? TotalCount { get; }

    /// <summary>
    /// Gets the value associated with the result.
    /// </summary>
    public TData Data { get; }
    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TData}"/> class for a successful operation.
    /// </summary>
    protected internal Result(TData data) : base(true)
    {
        Data = data;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TData}"/> class for a successful operation with total count.
    /// </summary>
    protected internal Result(TData data, int totalCount) : base(true)
    {
        Data = data;
        TotalCount = totalCount;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class for a failure operation.
    /// </summary>
    protected internal Result(TData data, string message) : base(false, message)
    {
        Data = data;
    }

    /// <summary>
    /// Implicitly converts a value to a result.
    /// If the value is not null, creates a successful result; otherwise, creates a failure result.
    /// </summary>
    public static implicit operator Result<TData>(TData? value) => Create(value);
}
