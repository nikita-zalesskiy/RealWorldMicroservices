using Marten.Schema;
using System.Text.Json.Serialization;

namespace Catalog.Api.Domain;

public sealed class Product
{
    public Product()
    {
        
    }

    /// <remarks>
    /// For data seeding purposes.
    /// </remarks>
    internal Product(Guid productId)
    {
        ProductId = productId;
    }

    [JsonInclude]
    public Guid ProductId { get; private set; }

    public required string Name;

    public required decimal Price;

    public required string ImageFile;
    
    public string? Description;

    public List<string> Categories = [];
}
