namespace WebBff.API.UseCases.Restaurants.GetAll
{
    public class GetAllRestaurantsHandler : IRequestHandler<GetAllRestaurants, IEnumerable<RestaurantViewModel>>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializeOptions;
        private readonly ILogger<GetAllRestaurantsHandler> _logger;

        public GetAllRestaurantsHandler(IHttpClientFactory clientFactory, 
                                        JsonSerializerOptions serializeOptions,
                                        ILogger<GetAllRestaurantsHandler> logger)
        {
            _client = clientFactory.CreateClient(ClientExtensions.RestaurantClient);
            _serializeOptions = serializeOptions;
            _logger = logger;
        }

        public async Task<IEnumerable<RestaurantViewModel>> Handle(GetAllRestaurants request, CancellationToken cancellationToken)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"?page={request.Page}&rows={request.Rows}");

            message.Headers.Add(RequestExtensions.CorrelationId, request.CorrelationId.ToString());

            var response = await _client.SendAsync(message, cancellationToken);

            if(response.IsSuccessStatusCode)
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
