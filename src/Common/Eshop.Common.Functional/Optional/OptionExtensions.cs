using Optional;
using Optional.Unsafe;
using System.Diagnostics.CodeAnalysis;

namespace Eshop.Common.Functional;

public static class OptionExtensions
{
    public static bool TryGetValue<TValue>(this Option<TValue> option, [NotNullWhen(returnValue: true)] out TValue? value)
    {
        value = option.ValueOrDefault();

        return option.HasValue;
    }

    public static bool TryGetValue<TValue, TException>(this Option<TValue, TException> option, [NotNullWhen(returnValue: true)] out TValue? value)
    {
        value = option.ValueOrDefault();

        return option.HasValue;
    }
}
