using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Common.Reflection;

public static class TypeExtensions
{
    public static bool IsOpenGeneric(this Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type.IsGenericTypeDefinition || type.ContainsGenericParameters;
    }

    public static bool IsAssignableToClosedVersionOf(this Type candidateType, Type openGeneric)
    {
        ArgumentNullException.ThrowIfNull(candidateType);

        ArgumentNullException.ThrowIfNull(openGeneric);

        if (!openGeneric.IsGenericTypeDefinition)
        {
            throw new ArgumentException(message: openGeneric.FullName);
        }

        if (candidateType.IsOpenGeneric())
        {
            return false;
        }

        return candidateType.GetSubtypes()
            .Any(type => type.IsClosedVersionOf(openGeneric));
    }

    public static bool IsClosedVersionOf(this Type candidateType, Type openGeneric)
    {
        ArgumentNullException.ThrowIfNull(candidateType);

        ArgumentNullException.ThrowIfNull(openGeneric);

        if (!openGeneric.IsGenericTypeDefinition)
        {
            throw new ArgumentException(message: openGeneric.FullName);
        }

        return candidateType.IsGenericType
            && openGeneric.Equals(candidateType.GetGenericTypeDefinition());
    }

    public static IEnumerable<Type> GetSubtypes(this Type type, bool skipCurrentType = false)
    {
        ArgumentNullException.ThrowIfNull(type);

        var skipCount = Convert.ToInt32(skipCurrentType);
        
        var interfaceTypes = type.GetInterfaces();

        return type
            .GetInheritanceHierarchy()
            .Concat(interfaceTypes)
            .Skip(skipCount);
    }

    public static IEnumerable<Type> GetInheritanceHierarchy(this Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        var currentType = type;

        while (currentType is not null)
        {
            yield return currentType;

            currentType = currentType.BaseType;
        }
    }
}
