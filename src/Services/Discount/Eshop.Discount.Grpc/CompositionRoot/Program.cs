namespace Eshop.Discount.Grpc;

public class Program
{
    public static void Main(string[] args)
    {
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(ConfigureWebHost)
            .Build()
            .Run();
    }

    private static void ConfigureWebHost(IWebHostBuilder webHostBuilder)
    {
        webHostBuilder.UseStartup<Startup>();
    }
}