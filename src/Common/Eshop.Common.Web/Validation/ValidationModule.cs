using Autofac;
using Autofac.Core;
using Eshop.Common.Reflection;
using Eshop.Common.Web.Functional;
using MediatR;

namespace Eshop.Common.Web.Validation;

public sealed class ValidationModule : Module
{
    private static readonly Type s_requestResultType = typeof(RequestResult<>);
    
    private static readonly Type s_validationBehaviorType = typeof(ValidationBehavior<,>);

    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterGeneric(s_validationBehaviorType)
            .InstancePerDependency();

        builder.RegisterGeneric(GetValidationBehavior)
            .As(typeof(IPipelineBehavior<,>))
            .InstancePerDependency();
    }

    private object GetValidationBehavior(IComponentContext context, Type[] types)
    {
        if (types is not [var requestType, var requestResultType]
            || !requestResultType.IsClosedVersionOf(s_requestResultType)
            || requestResultType.GetGenericArguments() is not [var responseType])
        {
            throw new DependencyResolutionException(message: nameof(GetValidationBehavior));
        }

        return context.Resolve(s_validationBehaviorType.MakeGenericType(requestType, responseType));
    }
}
