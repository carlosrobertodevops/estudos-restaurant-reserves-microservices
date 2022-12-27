namespace Restaurant.Application.Commands.UpdateRestaurant
{
    public sealed class UpdateRestaurantCommand : IUpdateRestaurantCommand
    {
        public Guid Id { get; private set; }
        public RestaurantViewModel Restaurant { get; private set; }
        public Guid CorrelationId { get; private set; }

        public UpdateRestaurantCommand(Guid id, RestaurantViewModel restaurant, Guid correlationId)
        {
            Id = id;
            Restaurant = restaurant;
            CorrelationId = correlationId;
        }
    }
}
