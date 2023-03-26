using Distributors.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Distributors.Api.Filters;

public class ModelValidationFilter : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var details = string.Join(" ", context.ModelState
                .SelectMany(keyValuePair => keyValuePair.Value!.Errors)
                .Select(modelError => modelError.ErrorMessage)
                .ToArray());
            throw new ValidationException(ExceptionCode.GeneralValidationException, details);
        }

        await next().ConfigureAwait(false);
    }
}