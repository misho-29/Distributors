using Distributors.Application.DisplayTools.Pagination;
using Distributors.Application.Models.Requests;
using Distributors.Application.Models.Responses.GetSales;
using Distributors.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Distributors.Api.Controllers;

public class SaleController : ApiBaseController
{
    private readonly ISaleService _saleService;

    public SaleController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedObject<SaleModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedObject<SaleModel>>> Get([FromQuery] GetSalesRequest request)
    {
        return Ok(await _saleService.GetAsync(request));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Create([FromBody] AddSaleRequest request)
    {
        await _saleService.AddAsync(request);
        return Ok();
    }
}
