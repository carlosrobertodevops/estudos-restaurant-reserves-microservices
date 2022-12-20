namespace WebBff.API.UseCases.Restaurants.GetById
{
    public class GetRestaurantByIdHandler : IRequestHandler<GetRestaurantById, RestaurantViewModel>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializeOptions;
        private readonly ILogger<GetRestaurantByIdHandler> _logger;

        public GetRestaurantByIdHandler(IHttpClientFactory clientFactory,
                                              JsonSerializerOptions serializeOptions,
                                              ILogger<GetRestaurantByIdHandler> logger)
        {
            _client = clientFactory.CreateClient(ClientExtensions.RestaurantClient);
            _serializeOptions = serializeOptions;
            _logger = logger;
        }

        public async Task<RestaurantViewModel> Handle(GetRestaurantById request, CancellationToken cancellationToken)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"{request.Id}");

            message.Headers.Add(RequestExtensions.CorrelationId, request.CorrelationId.ToString());

            var response = await _client.SendAsync(message, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Success", new { request, response, correlationId = request.CorrelationId });

                return await response.Content.ReadFromJsonAsync<RestaurantViewModel>(_serializeOptions, cancellationToken);
            }

            _logger.LogWarning("Error", new { request, response, correlationId = request.CorrelationId });

            var error = await response.Content.ReadFromJsonAsync<ErrorResponseViewModel>(_serializeOptions, cancellationToken);

            throw new BusinessException(error, response.StatusCode);
        }
    }
}
