using Autofac;

namespace Eshop.Basket.Api.Persistence;

public sealed class PersistenceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<ShoppingCartRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<RedisShoppingCartRepository>()
            .InstancePerLifetimeScope();
    }
}
