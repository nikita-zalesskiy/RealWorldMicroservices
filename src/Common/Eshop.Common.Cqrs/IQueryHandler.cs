using MediatR;

namespace Eshop.Common.Cqrs;

public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery>
    where TQuery : IQuery
{

}
