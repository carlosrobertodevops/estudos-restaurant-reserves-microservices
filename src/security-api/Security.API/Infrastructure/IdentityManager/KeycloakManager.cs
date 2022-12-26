namespace Security.API.IdentityManager
{
    public class KeycloakManager : IIdentityManager
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<KeycloakManager> _logger;
        private readonly JsonSerializerOptions _serializeOptions;

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

            _authEndpoint = _configuration["Keycloak:Endpoints:Login"];
        }

        public Task<AccessTokenViewModel> CreateAccountAsync(string username, string password, string firstName, string lastName, Guid correlationId, CancellationToken cancellationToken)
        {
            Console.WriteLine("Got here");

            return Task.FromResult(new AccessTokenViewModel());
        }

        public async Task<AccessTokenViewModel> LoginAsync(string username, string password, Guid correlationId, CancellationToken cancellationToken)
        {
            var body = LoginBody(username, password);

            var response = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Post, _authEndpoint)
            {
                Content = new FormUrlEncodedContent(body)
            },
            cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AccessTokenViewModel>(_serializeOptions, cancellationToken);
            }

            _logger.LogWarning("Error", new { response, correlationId = correlationId });

            var error = await response.Content.ReadFromJsonAsync<KeycloakErrorViewModel>(_serializeOptions, cancellationToken);

            throw new BusinessException(error.AsValidationErrors(), "Error with identity provider", response.StatusCode, correlationId);
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
    }
}
