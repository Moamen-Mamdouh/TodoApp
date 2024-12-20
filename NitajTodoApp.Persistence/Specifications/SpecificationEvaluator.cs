﻿using Microsoft.EntityFrameworkCore;
using NitajTodoApp.Domain.Primitives;
using NitajTodoApp.Domain.Specifications;

namespace NitajTodoApp.Persistence.Specifications;

internal static class SpecificationEvaluator<TEntity> where TEntity : ValueObject
{
    public static (IQueryable<TEntity> data, int count) GetQuery(
        IQueryable<TEntity> inputQuery,
        Specification<TEntity> specifications)
    {
        IQueryable<TEntity> queryable = inputQuery;

        int count = 0;

        if (specifications.IsGlobalFiltersIgnored)
            queryable = queryable.IgnoreQueryFilters();

        if (specifications.Criteria != null)
            queryable = queryable.Where(specifications.Criteria);

        if (specifications.OrderByDescendingExpression.Any())
        {
            var orderedQuery = queryable.OrderByDescending(specifications.OrderByDescendingExpression.First());

            foreach (var orderBy in specifications.OrderByDescendingExpression.Skip(1))
                orderedQuery = orderedQuery.ThenByDescending(orderBy);

            queryable = orderedQuery;
        }

        if (specifications.OrderByExpression.Any())
        {
            var orderedQuery = queryable.OrderBy(specifications.OrderByExpression.First());

            foreach (var orderBy in specifications.OrderByExpression.Skip(1))
                orderedQuery = orderedQuery.ThenBy(orderBy);

            queryable = orderedQuery;
        }

        if (specifications.IsDistinct)
            queryable = queryable.Distinct();

        if (specifications.IsTotalCountEnable)
            count = queryable.Count();

        if (specifications.IsPagingEnabled)
            queryable = queryable.Skip(specifications.Skip).Take(specifications.Take);

        if (specifications.Includes.Any())
            specifications.Includes.ForEach(x => queryable = queryable.Include(x));

        return (queryable, count);
    }
}