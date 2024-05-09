using AutoMapper;
using Catalog.Api.Domain;
using Eshop.Common.Web;

namespace Eshop.Catalog.Api.Features.UpdateProduct;

internal sealed class UpdateProductProfile : Profile
{
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductCommand, Product>()
            .Ignore(destination => destination.ProductId)
            .OptionMember(destination => destination.Name, source => source.Name)
            .OptionMember(destination => destination.Price, source => source.Price)
            .OptionMember(destination => destination.ImageFile, source => source.ImageFile)
            .OptionMember(destination => destination.Description, source => source.Description)
            .OptionMember(destination => destination.Categories, source => source.Categories);
    }
}
