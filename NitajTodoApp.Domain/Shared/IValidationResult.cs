namespace NitajTodoApp.Domain.Shared;

public interface IValidationResult
{
    string[] ErrorMessages { get; }
}
