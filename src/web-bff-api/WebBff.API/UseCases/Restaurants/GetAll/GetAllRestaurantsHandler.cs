using MediatR;
using System.Text.Json;
using WebBff.API.Exceptions;
using WebBff.API.Extensions;
using WebBff.API.ViewModels;

namespace WebBff.API.UseCases.Restaurants.GetAll
{
    public class GetAllRestaurantsHandler : IRequestHandler<GetAllRestaurants, IEnumerable<RestaurantViewModel>>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializeOptions;

        public GetAllRestaurantsHandler(IHttpClientFactory clientFactory, JsonSerializerOptions serializeOptions)
        {
            _client = clientFactory.CreateClient(ClientExtensions.RestaurantClient);
            _serializeOptions = serializeOptions;
        }

        public async Task<IEnumerable<RestaurantViewModel>> Handle(GetAllRestaurants request, CancellationToken cancellationToken)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, $"?page={request.Page}&rows={request.Rows}");

            var response = await _client.SendAsync(message, cancellationToken);

            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<RestaurantViewModel>>(_serializeOptions, cancellationToken);
            }

            var test = await response.Content.ReadAsStringAsync();

            var error = await response.Content.ReadFromJsonAsync<ErrorResponseViewModel>(_serializeOptions, cancellationToken);

            throw new BusinessException(error, response.StatusCode);
        }
    }
}
