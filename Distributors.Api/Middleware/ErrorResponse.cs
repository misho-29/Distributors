namespace Distributors.Api.Middleware;

public class ErrorResponse
{
    public int StatusCode { get; set; }

    public string? ErrorMessage { get; set; }

    public string? Details { get; set; }
}
