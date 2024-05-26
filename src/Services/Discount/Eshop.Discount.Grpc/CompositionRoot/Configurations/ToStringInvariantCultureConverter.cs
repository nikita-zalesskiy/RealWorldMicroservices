using AutoMapper;
using System.Globalization;

namespace Eshop.Discount.Grpc.CompositionRoot;

internal sealed class ToStringInvariantCultureConverter<TValue>
    : ITypeConverter<TValue, string>
    where TValue : IFormattable
{
    public string Convert(TValue source, string destination, ResolutionContext context)
    {
        return source.ToString(format: default, CultureInfo.InvariantCulture);
    }
}
