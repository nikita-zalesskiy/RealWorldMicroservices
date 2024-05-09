using AutoMapper;
using Eshop.Common.Functional;
using Optional;

namespace Eshop.Common.Web.Functional;

public sealed class OptionValueConverter<TSourceMember, TDestinationMember>
    : IValueConverter<Option<TSourceMember>, TDestinationMember>
    , IValueConverter<TSourceMember, Option<TDestinationMember>>
{
    public TDestinationMember Convert(Option<TSourceMember> sourceMember, ResolutionContext context)
    {
        sourceMember.TryGetValue(out var value);

        var mapper = context.Mapper;

        return mapper.Map<TSourceMember?, TDestinationMember>(value);
    }

    public Option<TDestinationMember> Convert(TSourceMember sourceMember, ResolutionContext context)
    {
        var mapper = context.Mapper;

        var destinationMember = mapper.Map<TSourceMember, TDestinationMember>(sourceMember);

        return Option.Some(destinationMember);
    }
}
