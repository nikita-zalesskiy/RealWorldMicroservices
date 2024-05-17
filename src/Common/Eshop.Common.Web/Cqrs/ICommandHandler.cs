using Eshop.Common.Web.Functional;
using MediatR;

namespace Eshop.Common.Cqrs;

public interface ICommandHandler<in TCommand>
    : IRequestHandler<TCommand, RequestResult<Unit>>
    where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, RequestResult<TResponse>>
    where TCommand : ICommand<TResponse>;
