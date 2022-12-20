namespace WebBff.API.UseCases.Restaurants.GetById
{
    public class GetRestaurantById : IUseCase<RestaurantViewModel>
    {
        public Guid Id { get; set; }
        public Guid CorrelationId { get; private set; }

        public GetRestaurantById(Guid id, Guid correlationId)
        {
            Id = id;
            CorrelationId = correlationId;
        }
    }
}
