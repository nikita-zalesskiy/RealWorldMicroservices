using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;

namespace Eshop.Discount.Grpc.CompositionRoot;

public static class ConfigurationExtensions
{
    public static PropertyBuilder<TProperty> Field<TEntity, TProperty>(
        this EntityTypeBuilder<TEntity> entityTypeBuilder
        , Expression<Func<TEntity, TProperty>> propertyExpression)
        where TEntity : class
    {
        return entityTypeBuilder.Property(propertyExpression)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
