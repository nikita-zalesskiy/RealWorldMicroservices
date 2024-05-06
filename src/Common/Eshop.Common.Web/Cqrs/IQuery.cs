using Eshop.Common.Web.Functional;
using MediatR;

namespace Eshop.Common.Cqrs;

public interface IQuery : IRequest;

public interface IQuery<TResponse> : IRequest<RequestResult<TResponse>>;

