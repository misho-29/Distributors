using Distributors.Application.Models.Requests;
using FluentValidation;

namespace Distributors.Application.Models.Validators;
public class CalculateBonusValidator : AbstractValidator<CalculateBonusRequest>
{
    public CalculateBonusValidator()
    {
        RuleFor(request => request).Must(x => x.ToDate >= x.FromDate).WithMessage("FromDate is greater than ToDate");
    }
}
