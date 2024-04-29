


using System.Reflection;

namespace Catalog.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services);

        var application = builder.Build();

        ConfigureEndpoints(application);

        application.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddMediatR(ConfigureMediatR);
        services.AddCarter();
    }

    private static void ConfigureEndpoints(WebApplication application)
    {
        application.MapCarter();
    }

    private static void ConfigureMediatR(MediatRServiceConfiguration configuration)
    {
        configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    }
}
