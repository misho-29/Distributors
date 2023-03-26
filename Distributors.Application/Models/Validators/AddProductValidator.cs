using Distributors.Application.Models.Requests;
using FluentValidation;

namespace Distributors.Application.Models.Validators;
public class AddProductValidator : AbstractValidator<AddProductRequest>
{
    public AddProductValidator()
    {
        RuleFor(request => request.Code).NotEmpty();
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Price).NotEmpty();
    }
}
