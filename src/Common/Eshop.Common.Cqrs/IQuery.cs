using MediatR;

namespace Eshop.Common.Cqrs;

public interface IQuery : IRequest;

public interface IQuery<out TResponse> : IRequest<TResponse>;

