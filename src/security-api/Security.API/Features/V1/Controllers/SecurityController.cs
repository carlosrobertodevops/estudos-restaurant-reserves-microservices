using Security.API.UseCases.Login;

namespace Security.API.Features.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/security")]
    [Produces("application/json")]
    public class SecurityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SecurityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// login to the application with user credentials
        /// </summary>
        /// <param name="user">username and password for login</param>
        /// <param name="cancellationToken"></param>
        /// <returns>access token for access to application</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccessTokenViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseViewModel))]
        public async Task<IActionResult> Login(UserViewModel user, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new Login(user, Request.GetCorrelationId()), cancellationToken);

            return Ok(response);
        }
    }
}
