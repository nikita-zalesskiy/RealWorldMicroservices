using System.Diagnostics.CodeAnalysis;

namespace Eshop.Common.Web;

public sealed class ValidationError
{
    public ValidationError()
    {

    }

    [SetsRequiredMembers]
    public ValidationError(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public required string ErrorMessage { get; init; }

    public override string ToString()
    {
        return ErrorMessage;
    }
}
