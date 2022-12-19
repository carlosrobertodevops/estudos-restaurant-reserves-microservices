using Microsoft.AspNetCore.Mvc;

namespace WebBff.API.Features.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/restaurants")]
    [Produces("application/json")]
    public class RestaurantsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await Task.Run(() => { return Ok(); });
        }
    }
}
