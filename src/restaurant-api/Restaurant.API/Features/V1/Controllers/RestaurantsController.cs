using Restaurant.Application.Commands.DeleteRestaurant;
using Restaurant.Application.Commands.UpdateRestaurant;
using Restaurant.Application.Queries.GetRestaurantById;
using Restaurant.Application.Queries.GetRestaurants;
using Restaurant.Application.Queries.GetRestaurantsByAddress;
using Restaurant.Application.Queries.GetRestaurantsByName;

namespace Restaurant.API.Features.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/restaurants")]
    [Produces("application/json")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RestaurantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// get all restaurants with no filters 
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
            var response = await _mediator.Send(new GetRestaurantsQuery(page, rows), cancellationToken);

            return Ok(response);
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
            var response = await _mediator.Send(new GetRestaurantByIdQuery(id), cancellationToken);

            return Ok(response);
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
        public async Task<IActionResult> GetByName(int page,
                                                   int rows, 
                                                   string name,
                                                   CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetRestaurantsByNameQuery(page, rows, name), cancellationToken);

            return Ok(response);
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
        public async Task<IActionResult> GetByAddress(int page,
                                                      int rows, 
                                                      string city, 
                                                      string neighborhood, 
                                                      string zone,                                                       
                                                      CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetRestaurantsByAddressQuery(page, 
                                                                                 rows,
                                                                                 city,
                                                                                 neighborhood, 
                                                                                 zone), 
                                                                                 cancellationToken);
            return Ok(response);
        }

        /// <summary>
        /// create a new restaurant
        /// </summary>
        /// <param name="restaurant">restaurant body</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RestaurantViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> Post(RestaurantViewModel restaurant, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new CreateRestaurantCommand(restaurant), cancellationToken);

            return CreatedAtAction(nameof(Post), response);
        }

        /// <summary>
        /// update restaurants informations
        /// </summary>
        /// <param name="id">restaurant id</param>
        /// <param name="restaurant">restaurant body</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> Put(Guid id, RestaurantViewModel restaurant, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateRestaurantCommand(id, restaurant), cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// delete restaurant
        /// </summary>
        /// <param name="id">restaurant id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteRestaurantCommand(id), cancellationToken);

            return NoContent();
        }
    }
}
