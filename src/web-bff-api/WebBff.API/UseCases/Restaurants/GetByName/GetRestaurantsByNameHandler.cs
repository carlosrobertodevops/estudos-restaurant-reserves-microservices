namespace WebBff.API.UseCases.Restaurants.GetByName
{
    public class GetRestaurantsByNameHandler : IRequestHandler<GetRestaurantsByName, IEnumerable<RestaurantViewModel>>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializeOptions;
        private readonly ILogger<GetRestaurantsByNameHandler> _logger;

        public GetRestaurantsByNameHandler(IHttpClientFactory clientFactory,
                                           JsonSerializerOptions serializeOptions,
                                           ILogger<GetRestaurantsByNameHandler> logger)
        {
            _client = clientFactory.CreateClient(ClientExtensions.RestaurantClient);
            _serializeOptions = serializeOptions;
            _logger = logger;
        }

        public async Task<IEnumerable<RestaurantViewModel>> Handle(GetRestaurantsByName request, CancellationToken cancellationToken)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"name?page={request.Page}&rows={request.Rows}&name={request.Name}");

            message.Headers.Add(RequestExtensions.CorrelationId, request.CorrelationId.ToString());

            var response = await _client.SendAsync(message, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Success", new { request, response, correlationId = request.CorrelationId });

                return await response.Content.ReadFromJsonAsync<IEnumerable<RestaurantViewModel>>(_serializeOptions, cancellationToken);
            }

            _logger.LogWarning("Error", new { request, response, correlationId = request.CorrelationId });

            var error = await response.Content.ReadFromJsonAsync<ErrorResponseViewModel>(_serializeOptions, cancellationToken);

            throw new BusinessException(error, response.StatusCode);
        }
    }
}
