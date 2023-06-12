using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace FleetJourney.Application.Extensions;

public static class DbContextExtensions
{
    public static Task LoadDataAsync<TEntity, TProperty>(this DbSet<TEntity> dbSet, TEntity entity,
        Expression<Func<TEntity, TProperty?>> expression)
        where TEntity : class
        where TProperty : class
    {
        return dbSet.Entry(entity)
            .Reference(expression)
            .LoadAsync();
    }

    public static Task LoadDataAsync<TEntity, TProperty>(this DbSet<TEntity> dbSet, TEntity entity,
        Expression<Func<TEntity, IEnumerable<TProperty>>> expression)
        where TEntity : class
        where TProperty : class
    {
        return dbSet.Entry(entity)
            .Collection(expression)
            .LoadAsync();
    }

    public static async Task LoadDataAsync<TEntity, TProperty>(this DbSet<TEntity> dbSet,
        IEnumerable<TEntity> entities,
        Expression<Func<TEntity, TProperty>> expression)
        where TEntity : class
        where TProperty : class
    {
        foreach (var entity in entities)
        {
            await dbSet.LoadDataAsync(entity, expression!);
        }
    }

    public static async Task LoadDataAsync<TEntity, TProperty>(this DbSet<TEntity> dbSet,
        IEnumerable<TEntity> entities,
        Expression<Func<TEntity, IEnumerable<TProperty>>> expression)
        where TEntity : class
        where TProperty : class
    {
        foreach (var entity in entities)
        {
            await dbSet.LoadDataAsync(entity, expression);
        }
    }

    public static async Task<bool> ExistsAsync<TEntity, TKey>(this DbSet<TEntity> entities,
        Expression<Func<TEntity, TKey>> idSelector, TKey idValue, CancellationToken cancellationToken)
        where TEntity : class
    {
        var predicate = BuildIdPredicate(idSelector, idValue);
        return await entities.AnyAsync(predicate, cancellationToken);
    }

    private static Expression<Func<TEntity, bool>> BuildIdPredicate<TEntity, TKey>(
        Expression<Func<TEntity, TKey>> idSelector, TKey idValue)
    {
        var parameter = idSelector.Parameters[0];
        var property = Expression.Property(parameter, GetPropertyName(idSelector));
        var equalExpression = Expression.Equal(property, Expression.Constant(idValue, typeof(TKey)));

        return Expression.Lambda<Func<TEntity, bool>>(equalExpression, parameter);
    }

    private static string GetPropertyName<T, TKey>(Expression<Func<T, TKey>> propertyExpression)
    {
        if (propertyExpression.Body is MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }

        throw new ArgumentException("Invalid property expression.");
    }
}