using Distributors.Application.Models.Requests;
using FluentValidation;

namespace Distributors.Application.Models.Validators;
public class AddSaleValidator : AbstractValidator<AddSaleRequest>
{
    public AddSaleValidator()
    {
        RuleFor(request => request.ProductCode).NotEmpty();
        RuleFor(request => request.DistributorId).NotEmpty();
        RuleFor(request => request.SaleDate).NotEmpty();
        RuleFor(request => request.Quantity).NotEmpty();
        RuleFor(request => request.TotalPrice).NotEmpty();
    }
}
