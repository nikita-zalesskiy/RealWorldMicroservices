using Eshop.Basket.Api.Domain;
using Marten;

namespace Eshop.Basket.Api.Persistence;

internal sealed class ShoppingCartRepository
{
    public ShoppingCartRepository(IDocumentSession documentSession)
    {
        _documentSession = documentSession;
    }

    private readonly IDocumentSession _documentSession;

    public Task<ShoppingCart?> GetShoppingCartAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return _documentSession.LoadAsync<ShoppingCart>(userId, cancellationToken);
    }

    public Task UpsertShoppingCartAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
    {
        _documentSession.Store(shoppingCart);

        return _documentSession.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteShoppingCartAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        _documentSession.Delete<ShoppingCart>(userId);

        return _documentSession.SaveChangesAsync(cancellationToken);
    }
}
