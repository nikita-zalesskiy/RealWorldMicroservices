using Microsoft.AspNetCore;

namespace Eshop.Catalog.Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebHost
            .CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build()
            .Run();
    }
}
