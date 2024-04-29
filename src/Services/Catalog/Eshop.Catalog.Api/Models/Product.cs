namespace Catalog.Api.Models;

public sealed class Product
{
    public Guid ProductId { get; private set; }

    public string? Name;

    public List<string> Categories { get; private set; } = [];

    public string? Description;

    public string? ImageFile;

    public decimal Price;
}
