using Distributors.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Distributors.Api.Controllers;

[ApiController]
[ModelValidationFilter]
[Produces("application/json")]
[Route("api/[controller]")]
public class ApiBaseController : ControllerBase
{
}
