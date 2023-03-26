using Distributors.Application.DisplayTools.Pagination;
using Distributors.Application.Models.Requests;
using Distributors.Application.Models.Responses;
using Distributors.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Distributors.Api.Controllers;

public class BonusController : ApiBaseController
{
    private readonly IBonusService _bonusService;

    public BonusController(IBonusService bonusService)
    {
        _bonusService = bonusService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedObject<DistributorBonusModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedObject<DistributorBonusModel>>> GetDistributorBonuses([FromQuery] GetDistributorBonusesRequest request)
    {
        return Ok(await _bonusService.GetDistributorBonusesAsync(request));
    }

    [HttpPost("Calculate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Calculate([FromQuery] CalculateBonusRequest request)
    {
        await _bonusService.CalculateAsync(request);
        return Ok();
    }
}
