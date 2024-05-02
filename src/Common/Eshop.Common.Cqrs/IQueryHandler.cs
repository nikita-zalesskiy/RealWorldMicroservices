using MediatR;

namespace Eshop.Common.Cqrs;

public interface IQueryHandler<in TQuery> : IRequestHandler<TQuery>
    where TQuery : IQuery;

public interface IQueryHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : IQuery<TResponse>;