using MediatR;
using WebBff.API.ViewModels;

namespace WebBff.API.UseCases.Restaurants.GetAll
{
    public class GetAllRestaurants : IUseCase<IEnumerable<RestaurantViewModel>>
    {
        public int Page { get; set; }
        public int Rows { get; set; }
        public Guid CorrelationId { get; private set; }

        public GetAllRestaurants(int page, int rows, Guid correlationId)
        {
            Page = page;
            Rows = rows;
            CorrelationId = correlationId;
        }
    }
}
