using Distributors.Application.Models.Requests.AddDistributor;
using FluentValidation;

namespace Distributors.Application.Models.Validators.AddDistributor;
public class IdentityDocumentValidator : AbstractValidator<IdentityDocument>
{
    public IdentityDocumentValidator()
    {
        RuleFor(request => request.Type).NotNull().IsInEnum();
        RuleFor(request => request.SerialNo).MaximumLength(50);
        RuleFor(request => request.DocumentNo).MaximumLength(50);
        RuleFor(request => request.DateOfIssue).NotEmpty();
        RuleFor(request => request.DateOfExpiry).NotEmpty();
        RuleFor(request => request.PersonalNo).NotEmpty().MaximumLength(50);
        RuleFor(request => request.IssuingAuthority).MaximumLength(100);
    }
}
