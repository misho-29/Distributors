using Distributors.Application.Models.Requests;
using Distributors.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Distributors.Api.Controllers;

public class ProductController : ApiBaseController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Create([FromBody] AddProductRequest request)
    {
        await _productService.AddAsync(request);
        return Ok();
    }
}
