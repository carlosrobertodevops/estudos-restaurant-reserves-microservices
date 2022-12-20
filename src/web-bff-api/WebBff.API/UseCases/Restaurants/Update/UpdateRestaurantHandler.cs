﻿using System.Text;

namespace WebBff.API.UseCases.Restaurants.Update
{
    public class UpdateRestaurantHandler : IRequestHandler<UpdateRestaurant>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializeOptions;
        private readonly ILogger<UpdateRestaurantHandler> _logger;

        public UpdateRestaurantHandler(IHttpClientFactory clientFactory,
                                       JsonSerializerOptions serializeOptions,
                                       ILogger<UpdateRestaurantHandler> logger)
        {
            _client = clientFactory.CreateClient(ClientExtensions.RestaurantClient);
            _serializeOptions = serializeOptions;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateRestaurant request, CancellationToken cancellationToken)
        {
            var message = new HttpRequestMessage(HttpMethod.Put, $"{request.Id}")
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

                return Unit.Value;
            }

            _logger.LogWarning("Error", new { request, response, correlationId = request.CorrelationId });

            var error = await response.Content.ReadFromJsonAsync<ErrorResponseViewModel>(_serializeOptions, cancellationToken);

            throw new BusinessException(error, response.StatusCode);
        }
    }
}
