namespace NitajTodoApp.Domain.Shared;

/// <summary>
/// Represents a validation result with errors and a value.
/// </summary>
public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    private ValidationResult(TValue value, string[] errors)
        : base(value, errors.Length > 0 ? errors[0] : "Validation failed")
    {
        ErrorMessages = errors;
    }

    /// <summary>
    /// Gets the errors associated with the validation result.
    /// </summary>
    public string[] ErrorMessages { get; }

    /// <summary>
    /// Creates a validation result with the specified value and errors.
    /// </summary>
    public static ValidationResult<TValue> WithErrors(TValue value, string[] errors) => new ValidationResult<TValue>(value, errors);
}
