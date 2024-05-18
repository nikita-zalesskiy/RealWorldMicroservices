using Catalog.Api.Domain;
using Marten;
using Marten.Schema;

namespace Eshop.Catalog.Api.DataSeeds;

public sealed class ProductDataSeed : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        var hasAnyProduct = await session
            .Query<Product>()
            .AnyAsync(cancellation);

        if (hasAnyProduct)
        {
            return;
        }

        var prodcuts = GetProducts();

        session.Store(prodcuts);

        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<Product> GetProducts()
    {
        return
        [
            new(new Guid("9e9f7ac4-2768-4e6c-af21-226f51faa511"))
            {
                Name = "Product One"
                , Price = 100m
                , ImageFile = "/images/product-one.png"
            }
            , new(new Guid("5e037cab-9888-4a55-9d18-468dc74fc99e"))
            {
                Name = "Product Two"
                , Price = 150m
                , ImageFile = "/images/product-two.png"
            }
            , new(new Guid("27cbb917-1c47-443e-bbea-a868f0d99da1"))
            {
                Name = "Product Three"
                , Price = 50m
                , ImageFile = "/images/product-three.png"
            }
        ];
    }
}
