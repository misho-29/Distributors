namespace Distributors.Core.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException(ExceptionCode code, string? details = null)
        : base(code.ToString())
    {
        Data.Add("Details", details);
    }
}