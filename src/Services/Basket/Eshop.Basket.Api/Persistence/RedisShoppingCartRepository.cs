using Eshop.Basket.Api.Domain;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Eshop.Basket.Api.Persistence;

internal sealed class RedisShoppingCartRepository
{
    public RedisShoppingCartRepository(
        IOptions<JsonOptions> jsonOptions
        , IDistributedCache distributedCache
        , ShoppingCartRepository shoppingCartRepository)
    {
        _distributedCache = distributedCache;
        
        _shoppingCartRepository = shoppingCartRepository;

        _jsonSerializerOptions = jsonOptions.Value.SerializerOptions;
    }

    private readonly IDistributedCache _distributedCache;
    
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    
    private readonly ShoppingCartRepository _shoppingCartRepository;

    public async Task<ShoppingCart?> GetShoppingCartAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var cacheKey = GetCacheKey(userId);

        var serializedShoppingCart = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);

        if (!string.IsNullOrEmpty(serializedShoppingCart))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(serializedShoppingCart, _jsonSerializerOptions);
        }

        var shoppingCart = await _shoppingCartRepository.GetShoppingCartAsync(userId, cancellationToken);

        await UpdateCache(shoppingCart, cancellationToken);

        return shoppingCart;
    }

    public Task UpsertShoppingCartAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
    {
        var upsertTask = _shoppingCartRepository.UpsertShoppingCartAsync(shoppingCart, cancellationToken);

        var cacheUpdateTask = UpdateCache(shoppingCart, cancellationToken);

        return Task.WhenAll(upsertTask, cacheUpdateTask);
    }

    public Task DeleteShoppingCartAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var cacheKey = GetCacheKey(userId);

        var deleteTask = _shoppingCartRepository.DeleteShoppingCartAsync(userId, cancellationToken);

        var updateCacheTask = _distributedCache.RemoveAsync(cacheKey);

        return Task.WhenAll(deleteTask, updateCacheTask);
    }

    private Task UpdateCache(ShoppingCart? shoppingCart, CancellationToken cancellationToken)
    {
        if (shoppingCart is null)
        {
            return Task.CompletedTask;
        }

        var cacheKey = GetCacheKey(shoppingCart.UserId);

        var serializedShoppingCart = JsonSerializer.Serialize(shoppingCart, _jsonSerializerOptions);

        return _distributedCache.SetStringAsync(cacheKey, serializedShoppingCart, cancellationToken);
    }

    private string GetCacheKey(Guid userId)
    {
        return userId.ToString();
    }
}
