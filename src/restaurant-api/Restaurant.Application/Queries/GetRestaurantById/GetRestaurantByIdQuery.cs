namespace Restaurant.Application.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQuery : IGetRestaurantByIdQuery<RestaurantViewModel>
    {
        public Guid Id { get; private set; }
        public Guid CorrelationId { get; private set; }

        public GetRestaurantByIdQuery(Guid id, Guid correlationId)
        {
            Id = id;
            CorrelationId = correlationId;
        }
    }
}
