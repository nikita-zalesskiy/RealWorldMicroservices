using Marten.Schema;
using System.Text.Json.Serialization;

namespace Catalog.Api.Models;

public sealed class Product
{
    [Identity]
    [JsonInclude]
    public Guid ProductId { get; private set; }

    public required string Name;

    public required decimal Price;

    public string? ImageFile;
    
    public string? Description;

    [JsonInclude]
    public List<string> Categories { get; private set; } = [];

    public void ReplaceCategories(IEnumerable<string> categories)
    {
        Categories.Clear();

        Categories.AddRange(categories);
    }
}
