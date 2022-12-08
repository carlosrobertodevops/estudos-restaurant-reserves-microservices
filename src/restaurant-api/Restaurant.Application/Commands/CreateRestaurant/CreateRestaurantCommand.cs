namespace Restaurant.Application.Commands.CreateRestaurant
{
    public class CreateRestaurantCommand : ICreateRestaurantCommand<RestaurantViewModel>
    {
        public RestaurantViewModel Restaurant { get; set; }
        public Guid CorrelationId { get; private set; }

        public CreateRestaurantCommand(RestaurantViewModel restaurant)
        {
            Restaurant = restaurant;
            CorrelationId = Guid.NewGuid();
        }
    }
}
