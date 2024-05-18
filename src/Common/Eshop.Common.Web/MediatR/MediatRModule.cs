using Autofac;
using Autofac.Core;
using Eshop.Common.Cqrs;
using Eshop.Common.Reflection;
using Eshop.Common.Web.Functional;
using MediatR;

namespace Eshop.Common.Web;

public sealed class MediatRModule : Module
{
    private static readonly Type s_commandType = typeof(ICommand<>);

    private static readonly Type s_requestResultType = typeof(RequestResult<>);

    private static readonly Type s_validationBehaviorType = typeof(ValidationBehavior<,>);

    private static readonly Type s_emptyPipelineBehaviorType = typeof(EmptyPipelineBehavior<,>);

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterGeneric(s_validationBehaviorType)
            .InstancePerDependency();

        builder.RegisterGeneric(GetValidationBehavior)
            .As(typeof(IPipelineBehavior<,>))
            .InstancePerDependency();
        
        builder.RegisterGeneric(typeof(LoggingBehavior<,>))
            .As(typeof(IPipelineBehavior<,>));
    }

    private object GetValidationBehavior(IComponentContext context, Type[] types)
    {   
        if (types is not [var requestType, var requestResultType])
        {
            throw new DependencyResolutionException(message: nameof(GetValidationBehavior));
        }

        Type behaviorType;

        if (!requestResultType.IsClosedVersionOf(s_requestResultType)
            || requestResultType.GetGenericArguments() is not [var responseType])
        {
            behaviorType = s_emptyPipelineBehaviorType.MakeGenericType(requestType, requestResultType);

            return Activator.CreateInstance(behaviorType)!;
        }

        behaviorType = s_validationBehaviorType.MakeGenericType(requestType, responseType);

        return context.Resolve(behaviorType);
    }
}
