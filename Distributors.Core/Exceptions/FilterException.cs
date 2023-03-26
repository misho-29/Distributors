namespace Distributors.Core.Exceptions;

public sealed class FilterException : Exception
{
    public FilterException(ExceptionCode code, string? details = null)
        : base(code.ToString())
    {
        Data.Add("Details", details);
    }
}