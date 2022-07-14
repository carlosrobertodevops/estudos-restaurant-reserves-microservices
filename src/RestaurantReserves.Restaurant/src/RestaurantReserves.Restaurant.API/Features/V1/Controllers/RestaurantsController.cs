using Microsoft.AspNetCore.Mvc;

namespace RestaurantReserves.Restaurant.API.Features.V1.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/restaurants")]
    [Produces("application/json")]
    public class RestaurantsController : ControllerBase
    {
        /// <summary>
        /// Test method.
        /// </summary>
        /// <returns>string</returns>
        [HttpGet("test")]
        public IResult TestApi()
        {
            return Results.Ok("Everything working");
        }
    }
}
