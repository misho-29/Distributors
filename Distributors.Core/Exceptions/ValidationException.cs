namespace Distributors.Core.Exceptions;

public sealed class ValidationException : Exception
{
    public ValidationException(ExceptionCode code, string? details = null)
        : base(code.ToString())
    {
        Data.Add("Details", details);
    }
}