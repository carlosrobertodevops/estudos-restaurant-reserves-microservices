using MediatR;
using WebBff.API.ViewModels;

namespace WebBff.API.UseCases.Restaurants.GetAll
{
    public class GetAllRestaurants : IRequest<IEnumerable<RestaurantViewModel>>
    {
        public int Page { get; set; }
        public int Rows { get; set; }

        public GetAllRestaurants(int page, int rows)
        {
            Page = page;
            Rows = rows;
        }
    }
}
