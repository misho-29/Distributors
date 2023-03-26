using Distributors.Core.DisplayTools.Pagination;
using FluentValidation;

namespace Distributors.Application.DisplayTools.Pagination;
public class PaginationValidator : AbstractValidator<PaginationObj>
{
    public PaginationValidator()
    {
        RuleFor(request => request.PageNumber).GreaterThan(0);
        RuleFor(request => request.PageSize).GreaterThan(0);
    }
}
