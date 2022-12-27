using FluentValidation;

namespace Security.API.UseCases.CreateUser
{
    public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, CreateUserEventResponse>
    {
        private readonly IIdentityManager _identityManager;
        private readonly IValidator<UserViewModel> _validator;
        private readonly ILogger<CreateUserRequestHandler> _logger;

        public CreateUserRequestHandler(IIdentityManager identityManager,
                                        IValidator<UserViewModel> validator,
                                        ILogger<CreateUserRequestHandler> logger)
        {
            _identityManager = identityManager;
            _validator = validator;
            _logger = logger;
        }

        public async Task<CreateUserEventResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var requestValidation = await _validator.ValidateAsync(request.User, cancellationToken);

            if (!requestValidation.IsValid)
            {
                _logger.LogWarning("Invalid user", new { request, requestValidation, correlationId = request.CorrelationId });

                return new CreateUserEventResponse(requestValidation, request.CorrelationId);
            }

            var accessToken = await _identityManager.CreateAccountAsync(request.User.Username,
                                                                        request.User.Password,
                                                                        request.User.FirstName,
                                                                        request.User.LastName,
                                                                        request.CorrelationId,
                                                                        cancellationToken);

            _logger.LogInformation("User created", new { request, accessToken, correlationId = request.CorrelationId });

            return new CreateUserEventResponse(accessToken, requestValidation, request.CorrelationId);
        }
    }
}
