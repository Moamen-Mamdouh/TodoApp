using FluentValidation;
using NitajTodoApp.Domain.Primitives;
using NitajTodoApp.Domain.Repositories;

namespace NitajTodoApp.Application.Extensions.FluentValidation;

public static class EntityExistValidator
{
    public static IRuleBuilderOptions<T, int> EntityExist<T, TEntity>(
        this IRuleBuilder<T, int> ruleBuilder,
        IGenericRepository<TEntity> entityRepo)
        where TEntity : Entity
    {
        return ruleBuilder
            .MustAsync(async (id, cancellationToken)
                => await entityRepo
                        .IsExistAsync(entity => entity.Id == id, cancellationToken));
    }
}