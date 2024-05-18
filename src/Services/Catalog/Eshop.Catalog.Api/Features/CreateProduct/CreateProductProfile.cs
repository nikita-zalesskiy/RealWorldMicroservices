using AutoMapper;
using Catalog.Api.Domain;

namespace Eshop.Catalog.Api.Features.CreateProduct;

public sealed class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Product>();
    }
}
