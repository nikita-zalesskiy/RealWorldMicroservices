using AutoMapper;
using Catalog.Api.Domain;
using Eshop.Common.Cqrs;
using Eshop.Common.Web.Functional;
using Marten;

namespace Eshop.Catalog.Api.Features.UpdateProduct;

internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductCommandResult>
{
    public UpdateProductCommandHandler(IMapper mapper
        , IDocumentSession documentSession)
    {
        _mapper = mapper;

        _documentSession = documentSession;
    }

    private readonly IMapper _mapper;

    private readonly IDocumentSession _documentSession;

    public async Task<RequestResult<UpdateProductCommandResult>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _documentSession.LoadAsync<Product>(command.ProductId, cancellationToken);

        if (product is null)
        {
            return new UpdateProductCommandResult(isSucceeded: false)
                .AsRequestResult();
        }

        _mapper.Map(command, product);

        _documentSession.Update(product);

        await _documentSession.SaveChangesAsync(cancellationToken);

        return new UpdateProductCommandResult(isSucceeded: true)
            .AsRequestResult();
    }
}
