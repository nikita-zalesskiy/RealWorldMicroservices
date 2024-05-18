using Autofac;
using Carter;
using Eshop.Common.Json;
using Eshop.Common.Web.Carter;
using Eshop.Common.Web.Functional;
using FluentValidation;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Eshop.Common.Web;

public static class ServiceCollectionExtensions
{
    public static void AddWebCommon(this IServiceCollection services, Assembly? assembly = default)
    {
        assembly ??= Assembly.GetCallingAssembly();

        services.Configure<JsonOptions>(ConfigureJsonOptions);

        services.AddAutoMapper(assembly);

        services.AddValidatorsFromAssembly(assembly);

        services.AddCarter(configurator: ConfigureCarter);
        
        services.AddMediatR(configuration => ConfigureMediatR(configuration, assembly));

        services.AddExceptionHandler<CommonExceptionHandler>();
    }

    private static void ConfigureJsonOptions(JsonOptions options)
    {
        var serializationOptions = options.SerializerOptions;

        var jsonConverters = serializationOptions.Converters;

        JsonSerializationConfigurator.Configure(serializationOptions);

        jsonConverters.Add(new OptionJsonConverterFactory());
    }

    private static void ConfigureMediatR(MediatRServiceConfiguration configuration, Assembly assembly)
    {
        configuration.RegisterServicesFromAssembly(assembly);
    }

    private static void ConfigureCarter(CarterConfigurator configurator)
    {
        configurator.WithEmptyValidators();

        configurator.WithResponseNegotiator<JsonResponseNegotiator>();
    }

    public static void ConfigureWebCommon(this ContainerBuilder builder)
    {
        builder.RegisterModule<FunctionalModule>();

        builder.RegisterModule<MediatRModule>();
    }
}
