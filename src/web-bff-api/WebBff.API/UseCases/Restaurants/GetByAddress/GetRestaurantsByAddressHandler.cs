namespace WebBff.API.UseCases.Restaurants.GetByAddress
{
    public class GetRestaurantsByAddressHandler : IRequestHandler<GetRestaurantsByAddress, IEnumerable<RestaurantViewModel>>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializeOptions;
        private readonly ILogger<GetRestaurantsByAddressHandler> _logger;

        public GetRestaurantsByAddressHandler(IHttpClientFactory clientFactory,
                                              JsonSerializerOptions serializeOptions,
                                              ILogger<GetRestaurantsByAddressHandler> logger)
        {
            _client = clientFactory.CreateClient(ClientExtensions.RestaurantClient);
            _serializeOptions = serializeOptions;
            _logger = logger;
        }

        public async Task<IEnumerable<RestaurantViewModel>> Handle(GetRestaurantsByAddress request, CancellationToken cancellationToken)
        {
            var url = $"address?";

            foreach (var property in request.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if(property.Name != nameof(request.CorrelationId) && property.GetValue(request, null) is not null)
                {
                    url += $"{property.Name.ToLower()}={property.GetValue(request, null)}&";
                }
            }

            var message = new HttpRequestMessage(HttpMethod.Get, url[..^1]);

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
