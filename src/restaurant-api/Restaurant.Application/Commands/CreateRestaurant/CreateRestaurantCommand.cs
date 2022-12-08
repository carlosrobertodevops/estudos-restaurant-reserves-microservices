namespace Restaurant.Application.Commands.CreateRestaurant
{
    public class CreateRestaurantCommand : ICreateRestaurantCommand<RestaurantViewModel>
    {
        public RestaurantViewModel Restaurant { get; set; }

        public CreateRestaurantCommand(RestaurantViewModel restaurant)
        {
            Restaurant = restaurant;
        }
    }
}
