using Eshop.Common.Web.Functional;
using MediatR;

namespace Eshop.Common.Cqrs;

public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery>
    where TQuery : IQuery;

public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, RequestResult<TResponse>>
    where TQuery : IQuery<TResponse>;