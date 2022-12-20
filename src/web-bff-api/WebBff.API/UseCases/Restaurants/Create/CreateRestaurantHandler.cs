using System.Text;

namespace WebBff.API.UseCases.Restaurants.Create
{
    public class CreateRestaurantHandler : IRequestHandler<CreateRestaurant, RestaurantViewModel>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializeOptions;
        private readonly ILogger<CreateRestaurantHandler> _logger;

        public CreateRestaurantHandler(IHttpClientFactory clientFactory,
                                       JsonSerializerOptions serializeOptions,
                                       ILogger<CreateRestaurantHandler> logger)
        {
            _client = clientFactory.CreateClient(ClientExtensions.RestaurantClient);
            _serializeOptions = serializeOptions;
            _logger = logger;
        }
       
        public async Task<RestaurantViewModel> Handle(CreateRestaurant request, CancellationToken cancellationToken)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, string.Empty)
            {
                Content = new StringContent(JsonSerializer.Serialize(request.RestaurantViewModel, _serializeOptions), 
                                            Encoding.UTF8, 
                                            "application/json")
            };

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
