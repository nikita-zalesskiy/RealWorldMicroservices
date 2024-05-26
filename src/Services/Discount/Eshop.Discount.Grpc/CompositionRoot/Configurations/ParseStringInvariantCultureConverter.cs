using AutoMapper;
using System.Globalization;

namespace Eshop.Discount.Grpc.CompositionRoot;

internal sealed class ParseStringInvariantCultureConverter<TValue>
    : ITypeConverter<string, TValue>
    where TValue : IParsable<TValue>
{
    public TValue Convert(string source, TValue destination, ResolutionContext context)
    {
        return TValue.Parse(source, CultureInfo.InvariantCulture);
    }
}
