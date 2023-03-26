using Distributors.Application.DisplayTools.Pagination;
using Distributors.Application.Models.Requests;
using Distributors.Application.Models.Requests.AddDistributor;
using Distributors.Application.Models.Responses;
using Distributors.Application.Services;
using Distributors.Core.DisplayTools.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Distributors.Api.Controllers;

public class DistributorController : ApiBaseController
{
    private readonly IDistributorService _distributorService;

    public DistributorController(IDistributorService distributorService)
    {
        _distributorService = distributorService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Create([FromBody] AddDistributorRequest request)
    {
        await _distributorService.AddAsync(request);
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Delete([FromRoute] string id)
    {
        await _distributorService.DeleteAsync(id);
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedObject<DistributorModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedObject<DistributorModel>>> GetAll([FromQuery] PaginationObj pagination)
    {
        return Ok(await _distributorService.GetAsync(pagination));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Update([FromRoute] string id, [FromBody] UpdateDistributorRequest request)
    {
        await _distributorService.UpdateAsync(id, request);
        return Ok();
    }
}
