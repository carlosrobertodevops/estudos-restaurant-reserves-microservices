namespace WebBff.API.UseCases.Restaurants.Delete
{
    public class DeleteRestaurantHandler : IRequestHandler<DeleteRestaurant>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializeOptions;
        private readonly ILogger<DeleteRestaurantHandler> _logger;

        public DeleteRestaurantHandler(IHttpClientFactory clientFactory,
                                       JsonSerializerOptions serializeOptions,
                                       ILogger<DeleteRestaurantHandler> logger)
        {
            _client = clientFactory.CreateClient(ClientExtensions.RestaurantClient);
            _serializeOptions = serializeOptions;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteRestaurant request, CancellationToken cancellationToken)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, $"{request.Id}");

            message.Headers.Add(RequestExtensions.CorrelationId, request.CorrelationId.ToString());

            var response = await _client.SendAsync(message, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Success", new { request, response, correlationId = request.CorrelationId });

                return Unit.Value;
            }

            _logger.LogWarning("Error", new { request, response, correlationId = request.CorrelationId });

            var error = await response.Content.ReadFromJsonAsync<ErrorResponseViewModel>(_serializeOptions, cancellationToken);

            throw new BusinessException(error, response.StatusCode);
        }
    }
}
