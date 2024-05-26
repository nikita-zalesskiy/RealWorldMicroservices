using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Eshop.Common.Web;

public static class ApplicationBuilderExtensions
{
    public static void UseWebCommon(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseExceptionHandler(delegate { });

        applicationBuilder.UseRouting();

        applicationBuilder.UseEndpoints(ConfigureEndpoints);
    }

    private static void ConfigureEndpoints(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapCarter();
    }
}
