namespace Restaurant.API.Features.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/restaurants")]
    [Produces("application/json")]
    public class RestaurantsController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> Get(int page, int rows, CancellationToken cancellationToken)
        {
            return Ok(string.Empty);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            return Ok(string.Empty);
        }

        [HttpGet("name")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> GetByName(string name, CancellationToken cancellationToken)
        {
            return Ok(string.Empty);
        }

        [HttpGet("address")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> GetByAddress(string postalCode, 
                                                      string city, 
                                                      string neighborhood, 
                                                      string zone, 
                                                      CancellationToken cancellationToken)
        {
            return Ok(string.Empty);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RestaurantViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> Post(CancellationToken cancellationToken)
        {
            return Ok(string.Empty);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> Put(CancellationToken cancellationToken)
        {
            return Ok(string.Empty);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> Delete(CancellationToken cancellationToken)
        {
            return Ok(string.Empty);
        }
    }
}
