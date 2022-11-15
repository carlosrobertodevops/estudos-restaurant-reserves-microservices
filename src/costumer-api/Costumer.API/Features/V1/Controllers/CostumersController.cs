namespace Costumer.API.Features.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/costumers")]
    [Produces("application/json")]
    public class CostumersController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> Get()
        {
            return Ok(string.Empty);
        }
    }
}
