using Autofac;

namespace Eshop.Common.Web.Functional;

public sealed class FunctionalModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        RegisterOptionalDependencies(builder);
    }

    private static void RegisterOptionalDependencies(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(OptionValueConverter<,>));
    }
}
