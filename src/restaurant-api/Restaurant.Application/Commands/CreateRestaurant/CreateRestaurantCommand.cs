namespace Restaurant.Application.Commands.CreateRestaurant
{
    public sealed class CreateRestaurantCommand : ICreateRestaurantCommand<RestaurantViewModel>
    {
        public RestaurantViewModel Restaurant { get; set; }
        public Guid CorrelationId { get; private set; }

        public CreateRestaurantCommand(RestaurantViewModel restaurant, Guid correlationId)
        {
            Restaurant = restaurant;
            CorrelationId = correlationId;
        }
    }
}
