using MediatR;

namespace Eshop.Common.Cqrs;

public interface ICommand : IRequest;

public interface ICommand<out TResponse> : IRequest<TResponse>;