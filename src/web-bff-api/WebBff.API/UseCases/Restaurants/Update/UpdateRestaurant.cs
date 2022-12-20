namespace WebBff.API.UseCases.Restaurants.Update
{
    public class UpdateRestaurant : IUseCase
    {
        public Guid Id { get; }
        public RestaurantViewModel RestaurantViewModel { get; }
        public Guid CorrelationId { get; }

        public UpdateRestaurant(Guid id, RestaurantViewModel restaurantViewModel, Guid correlationId)
        {
            Id = id;
            RestaurantViewModel = restaurantViewModel;
            CorrelationId = correlationId;
        }
    }
}
