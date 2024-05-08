using Autofac.Extensions.DependencyInjection;

namespace Eshop.Catalog.Api;

public class Program
{
    public static void Main(string[] args)
    {
        Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(ConfigureWebHost)
            .Build()
            .Run();
    }

    private static void ConfigureWebHost(IWebHostBuilder webHostBuilder)
    {
        webHostBuilder.UseStartup<Startup>();
    }
}
