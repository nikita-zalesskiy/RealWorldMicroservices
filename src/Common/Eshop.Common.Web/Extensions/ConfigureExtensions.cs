using Autofac;
using Carter;
using Eshop.Common.Json;
using Eshop.Common.Web.Carter;
using Eshop.Common.Web.Functional;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Common.Web;

public static class ConfigureExtensions
{
    public static void AddWebCommon(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(ConfigureJsonOptions);

        services.AddCarter(configurator: ConfigureCarter);
    }

    private static void ConfigureJsonOptions(JsonOptions options)
    {
        var serializationOptions = options.SerializerOptions;

        var jsonConverters = serializationOptions.Converters;

        JsonSerializationConfigurator.Configure(serializationOptions);

        jsonConverters.Add(new OptionJsonConverterFactory());
    }

    private static void ConfigureCarter(CarterConfigurator configurator)
    {
        configurator.WithResponseNegotiator<JsonResponseNegotiator>();
    }

    public static void ConfigureWebCommon(this ContainerBuilder builder)
    {
        builder.RegisterModule<FunctionalModule>();

        builder.RegisterModule<ValidationModule>();
    }
}
