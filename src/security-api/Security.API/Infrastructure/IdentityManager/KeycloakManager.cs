using System.Text;

namespace Security.API.IdentityManager
{
    public class KeycloakManager : IIdentityManager
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<KeycloakManager> _logger;
        private readonly JsonSerializerOptions _serializeOptions;

        private readonly string _createUserEndpoint;
        private readonly string _authEndpoint;

        public KeycloakManager(IHttpClientFactory clientFactory,
                               IConfiguration configuration,
                               ILogger<KeycloakManager> logger,
                               JsonSerializerOptions serializeOptions)
        {
            _client = clientFactory.CreateClient(ClientExtensions.KeycloakClient);
            _configuration = configuration;
            _logger = logger;
            _serializeOptions = serializeOptions;

            _createUserEndpoint = _configuration["IdentityManager:Endpoints:CreateUser"];
            _authEndpoint = _configuration["IdentityManager:Endpoints:Login"];
        }

        public async Task<AccessTokenViewModel> CreateAccountAsync(UserRepresentation user,
                                                             string accessToken,
                                                             Guid correlationId,
                                                             CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _createUserEndpoint)
            {
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(user, _serializeOptions),
                                            Encoding.UTF8,
                                            "application/json")
            };

            request.Headers.Add("Authorization", $"Bearer {accessToken}");

            return await SendAsync<AccessTokenViewModel>(request, correlationId, cancellationToken);
        }

        public async Task<AccessTokenViewModel> LoginAsync(string username,
                                                           string password,
                                                           Guid correlationId,
                                                           CancellationToken cancellationToken)
        {
            var body = LoginBody(username, password);

            return await SendAsync<AccessTokenViewModel>(new HttpRequestMessage(HttpMethod.Post, _authEndpoint)
            {
                Content = new FormUrlEncodedContent(body)
            },
            correlationId,
            cancellationToken);
        }

        private IDictionary<string, string> LoginBody(string username = null, string password = null)
        {
            var body = new Dictionary<string, string>
            {
                {"client_id",  _configuration["Keycloak:ClientId"]},
                {"client_secret",  _configuration["Keycloak:ClientSecret"]},
                {"grant_type",  _configuration["Keycloak:GrantType"]}
            };

            if (!string.IsNullOrWhiteSpace(username))
            {
                body.Add(nameof(username), username);
            }

            if (!string.IsNullOrWhiteSpace(password))
            {
                body.Add(nameof(password), password);
            }

            return body;
        }

        private async Task<TResponse> SendAsync<TResponse>(HttpRequestMessage request, Guid correlationId, CancellationToken cancellationToken)
        {
            var response = await _client.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TResponse>(_serializeOptions, cancellationToken);
            }

            _logger.LogWarning("Error", new { response, correlationId });

            var error = await response.Content.ReadFromJsonAsync<KeycloakErrorViewModel>(_serializeOptions, cancellationToken);

            throw new BusinessException(error.AsValidationErrors(), "Error with identity provider", response.StatusCode, correlationId);
        }
    }
}
