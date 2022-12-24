using Security.API.Exceptions;

namespace Security.API.UseCases.Login
{
    public class LoginHandler : IRequestHandler<Login, AccessTokenViewModel>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializeOptions;
        private readonly ILogger<LoginHandler> _logger;
        private readonly IConfiguration _configuration;

        public LoginHandler(IHttpClientFactory clientFactory,
                            JsonSerializerOptions serializeOptions,
                            ILogger<LoginHandler> logger,
                            IConfiguration configuration)
        {
            _client = clientFactory.CreateClient(ClientExtensions.KeycloakClient);
            _serializeOptions = serializeOptions;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<AccessTokenViewModel> Handle(Login request, CancellationToken cancellationToken)
        {
            var body = new Dictionary<string, string>
            {
                {"client_id",  _configuration["Keycloak:ClientId"]},
                {"client_secret",  _configuration["Keycloak:ClientSecret"]},
                {"grant_type",  _configuration["Keycloak:GrantType"]},
                {"username", request.User.Username },
                {"password", request.User.Password }
            };

            var response = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "protocol/openid-connect/token")
            {
                Content = new FormUrlEncodedContent(body)
            },
            cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Success", new { request, response, correlationId = request.CorrelationId });

                return await response.Content.ReadFromJsonAsync<AccessTokenViewModel>(_serializeOptions, cancellationToken);
            }

            _logger.LogWarning("Error", new { request, response, correlationId = request.CorrelationId });

            var error = await response.Content.ReadFromJsonAsync<KeycloakErrorViewModel>(_serializeOptions, cancellationToken);

            throw new BusinessException(error.AsValidationErrors(), "Error with identity provider", response.StatusCode, request.CorrelationId);
        }
    }
}
