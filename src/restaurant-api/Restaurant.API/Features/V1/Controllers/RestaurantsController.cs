namespace Restaurant.API.Features.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/restaurants")]
    [Produces("application/json")]
    public class RestaurantsController : ControllerBase
    {
        /// <summary>
        /// endpoint to get all restaurants with no filters 
        /// </summary>
        /// <param name="page">page number</param>
        /// <param name="rows">number of restaurants per page</param>
        /// <param name="cancellationToken"></param>
        /// <returns>list of restaurants</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantViewModel[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> Get(int page, int rows, CancellationToken cancellationToken)
        {
            return await Task.Run(() => Ok(string.Empty));
        }

        /// <summary>
        /// get restaurants by id
        /// </summary>
        /// <param name="id">id of the restaurant</param>
        /// <param name="cancellationToken"></param>
        /// <returns>one restaurant</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await Task.Run(() => Ok(string.Empty));
        }

        /// <summary>
        /// get restaurants by name
        /// </summary>
        /// <param name="name">name of restaurant</param>
        /// <param name="page">page number</param>
        /// <param name="rows">number of restaurants per page</param>
        /// <param name="cancellationToken"></param>
        /// <returns>list of restaurants</returns>
        [HttpGet("name")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantViewModel[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> GetByName(string name,
                                                   int page,
                                                   int rows, 
                                                   CancellationToken cancellationToken)
        {
            return await Task.Run(() => Ok(string.Empty));
        }

        /// <summary>
        /// get restaurants by address
        /// </summary>
        /// <param name="city">city of restaurant</param>
        /// <param name="neighborhood">neighborhood of the restaurant</param>
        /// <param name="zone">zone of the restaurant</param>
        /// <param name="page">page number</param>
        /// <param name="rows">number of restaurants per page</param>
        /// <param name="cancellationToken"></param>
        /// <returns>list of restaurants</returns>
        [HttpGet("address")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantViewModel[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> GetByAddress(string city, 
                                                      string neighborhood, 
                                                      string zone, 
                                                      int page,
                                                      int rows,
                                                      CancellationToken cancellationToken)
        {
            return await Task.Run(() => Ok(string.Empty));
        }

        /// <summary>
        /// create a new restaurant
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RestaurantViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> Post(CancellationToken cancellationToken)
        {
            return await Task.Run(() => Ok(string.Empty));
        }

        /// <summary>
        /// update restaurants informations
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> Put(CancellationToken cancellationToken)
        {
            return await Task.Run(() => Ok(string.Empty));
        }

        /// <summary>
        /// delete restaurant
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> Delete(CancellationToken cancellationToken)
        {
            return await Task.Run(() => Ok(string.Empty));
        }
    }
}
