using Autofac;

namespace Eshop.Common.Web.Functional;

public sealed class FunctionalModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        RegisterOptionDependencies(builder);
    }

    private static void RegisterOptionDependencies(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(OptionValueConverter<,>));
    }
}
