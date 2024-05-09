using AutoMapper;
using Eshop.Common.Expressions;
using Eshop.Common.Web.Functional;
using Optional;
using System.Linq.Expressions;

namespace Eshop.Common.Web;

public static class AutoMapperExtensions
{
    public static void MapFromOption<TSource, TDestination, TSourceMember, TDestinationMember>(
        this IMemberConfigurationExpression<TSource, TDestination, TDestinationMember> options
        , Expression<Func<TSource, Option<TSourceMember>>> sourceMember)
    {
        var precondition = ExpressionTransformer
            .Combine(sourceMember, option => option.HasValue)
            .Compile();

        options.PreCondition(precondition);

        options.ConvertUsing<OptionValueConverter<TSourceMember, TDestinationMember>, Option<TSourceMember>>(sourceMember);
    }

    public static void MapToOption<TSource, TDestination, TSourceMember, TDestinationMember>(
        this IMemberConfigurationExpression<TSource, TDestination, Option<TDestinationMember>> options
        , Expression<Func<TSource, TSourceMember>> sourceMember)
    {
        ArgumentNullException.ThrowIfNull(options);

        options.ConvertUsing<OptionValueConverter<TSourceMember, TDestinationMember>, TSourceMember>(sourceMember);
    }

    public static void MapToOption<TSource, TDestination, TMember>(
        this IMemberConfigurationExpression<TSource, TDestination, Option<TMember>> options)
    {
        ArgumentNullException.ThrowIfNull(options);

        options.ConvertUsing<OptionValueConverter<TMember, TMember>, TMember>();
    }

    public static IMappingExpression<TSource, TDestination> OptionMember<TSource, TDestination, TSourceMember, TDestinationMember>(
        this IMappingExpression<TSource, TDestination> mappingExpression
        , Expression<Func<TDestination, TDestinationMember>> destinationMember
        , Expression<Func<TSource, Option<TSourceMember>>> sourceMember)
    {
        ArgumentNullException.ThrowIfNull(mappingExpression);

        ArgumentNullException.ThrowIfNull(destinationMember);

        return mappingExpression.ForMember(destinationMember, options => options.MapFromOption(sourceMember));
    }

    public static IMappingExpression<TSource, TDestination> OptionMember<TSource, TDestination, TMember>(
        this IMappingExpression<TSource, TDestination> mappingExpression
        , Expression<Func<TDestination, Option<TMember>>> destinationMember)
    {
        ArgumentNullException.ThrowIfNull(mappingExpression);

        ArgumentNullException.ThrowIfNull(destinationMember);

        return mappingExpression.ForMember(destinationMember, options => options.MapToOption());
    }

    public static IMappingExpression<TSource, TDestination> OptionMember<TSource, TDestination, TSourceMember, TDestinationMember>(
        this IMappingExpression<TSource, TDestination> mappingExpression
        , Expression<Func<TDestination, Option<TDestinationMember>>> destinationMember
        , Expression<Func<TSource, TSourceMember>> sourceMember)
    {
        ArgumentNullException.ThrowIfNull(mappingExpression);

        ArgumentNullException.ThrowIfNull(destinationMember);

        return mappingExpression.ForMember(destinationMember, options => options.MapToOption(sourceMember));
    }

    public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination, TMember>(
        this IMappingExpression<TSource, TDestination> mappingExpression
        , Expression<Func<TDestination, TMember>> destinationMemberExpression)
    {
        ArgumentNullException.ThrowIfNull(mappingExpression);

        ArgumentNullException.ThrowIfNull(destinationMemberExpression);

        return mappingExpression.ForMember(destinationMemberExpression, options => options.Ignore());
    }
}
