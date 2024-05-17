using Eshop.Common.Web.Functional;
using MediatR;

namespace Eshop.Common.Cqrs;

public interface ICommand : IRequest<RequestResult<Unit>>;

public interface ICommand<TResponse> : IRequest<RequestResult<TResponse>>;