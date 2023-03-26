using Distributors.Application.Models.Requests;
using FluentValidation;

namespace Distributors.Application.Models.Validators;
public class UpdateDistributorValidator : AbstractValidator<UpdateDistributorRequest>
{
    public UpdateDistributorValidator()
    {
        RuleFor(request => request.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(request => request.LastName).NotEmpty().MaximumLength(50);
        RuleFor(request => request.DateOfBirth).NotEmpty();
        RuleFor(request => request.Gender).NotNull();
        RuleFor(request => request.ContactType).NotNull();
        RuleFor(request => request.ContactInfo).NotEmpty().MaximumLength(100);
        RuleFor(request => request.AddressType).NotNull();
        RuleFor(request => request.AddressInfo).NotEmpty().MaximumLength(100);
    }
}
