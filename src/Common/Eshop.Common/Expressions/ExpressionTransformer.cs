using System.Linq.Expressions;

namespace Eshop.Common.Expressions;

public static class ExpressionTransformer
{
    public static Expression<Func<TOuter, TInner>> Combine<TOuter, TMiddle, TInner>(
        Expression<Func<TOuter, TMiddle>> first
        , Expression<Func<TMiddle, TInner>> second)
    {
        ArgumentNullException.ThrowIfNull(first);

        ArgumentNullException.ThrowIfNull(second);

        var parameter = Expression.Parameter(typeof(TOuter), "outer");

        var firstInvoke = Expression.Invoke(first, [parameter]);
        
        var secondInvoke = Expression.Invoke(second, [firstInvoke]);

        return Expression.Lambda<Func<TOuter, TInner>>(secondInvoke, parameter);
    }
}
