using System.Collections;

namespace Eshop.Common.Web;

public sealed class ValidationErrorCollection : IEnumerable<ValidationError>
{
    public ValidationErrorCollection(IEnumerable<ValidationError> validationErrors)
    {
        _validationErrors = validationErrors;
    }

    public ValidationErrorCollection(ValidationError validationError)
    {
        _validationErrors = [validationError];
    }

    private readonly IEnumerable<ValidationError> _validationErrors;

    public override string ToString()
    {
        var errorMessages = _validationErrors.Select(validationError => validationError.ErrorMessage);

        return string.Join(Environment.NewLine, errorMessages);
    }

    public IEnumerator<ValidationError> GetEnumerator()
    {
        return _validationErrors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
