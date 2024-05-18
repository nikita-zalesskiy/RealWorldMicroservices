using AutoMapper;
using Catalog.Api.Domain;
using Eshop.Common.Cqrs;
using Eshop.Common.Web.Functional;
using Marten;

namespace Eshop.Catalog.Api.Features.CreateProduct;

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductCommandResult>
{
    public CreateProductCommandHandler(IMapper mapper
        , IDocumentSession documentSession)
    {
        _mapper = mapper;

        _documentSession = documentSession;
    }

    private readonly IMapper _mapper;
    
    private readonly IDocumentSession _documentSession;

    public async Task<RequestResult<CreateProductCommandResult>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<CreateProductCommand, Product>(command);

        _documentSession.Store(product);

        await _documentSession.SaveChangesAsync(cancellationToken);

        var commandResult = new CreateProductCommandResult(product.ProductId);

        return commandResult.AsRequestResult();
    }
}
