namespace Restaurant.Application.Commands.CreateRestaurant
{
    public class CreateRestaurantCommand : IRequest<RestaurantViewModel>
    {
        public RestaurantViewModel Restaurant { get; set; }

        public CreateRestaurantCommand(RestaurantViewModel restaurant)
        {
            Restaurant = restaurant;
        }
    }
}
