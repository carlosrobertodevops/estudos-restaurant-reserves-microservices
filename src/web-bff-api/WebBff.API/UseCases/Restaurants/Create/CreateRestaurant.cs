namespace WebBff.API.UseCases.Restaurants.Create
{
    public class CreateRestaurant : IUseCase<RestaurantViewModel>
    {
        public RestaurantViewModel RestaurantViewModel { get; set; }
        public Guid CorrelationId {get; set;}

        public CreateRestaurant(RestaurantViewModel restaurantViewModel, Guid correlationId)
        {
            RestaurantViewModel = restaurantViewModel;
            CorrelationId = correlationId;
        }
    }
}
