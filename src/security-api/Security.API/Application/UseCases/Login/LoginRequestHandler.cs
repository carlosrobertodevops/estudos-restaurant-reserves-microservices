namespace Security.API.UseCases.Login
{
    public class LoginRequestHandler : IRequestHandler<LoginRequest, AccessTokenViewModel>
    {
        private readonly IIdentityManager _identityManager;
        private readonly ILogger<LoginRequestHandler> _logger;

        public LoginRequestHandler(IIdentityManager identityManager,
                            ILogger<LoginRequestHandler> logger)
        {
            _identityManager = identityManager;
            _logger = logger;
        }

        public async Task<AccessTokenViewModel> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var response = await _identityManager.LoginAsync(request.User.Username,
                                                             request.User.Password,
                                                             request.CorrelationId,
                                                             cancellationToken);

            _logger.LogInformation("Success", new { request, response, correlationId = request.CorrelationId });

            return response;
        }
    }
}
