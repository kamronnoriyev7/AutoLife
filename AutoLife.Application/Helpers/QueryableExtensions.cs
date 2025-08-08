using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.Helpers;

public static class QueryableExtensions
{
    public static IQueryable<T> OnlyActive<T>(this IQueryable<T> source) where T : class
    {
        var param = Expression.Parameter(typeof(T), "x");
        var prop = Expression.Property(param, "IsDeleted");
        var notDeleted = Expression.Equal(prop, Expression.Constant(false));
        var lambda = Expression.Lambda<Func<T, bool>>(notDeleted, param);

        return source.Where(lambda);
    }
}
